using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Configuration;
using Dapper.FastCrud;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using NLog;
using PhotoCollection.Models;

namespace PhotoCollection.Helpers
{
    public class DbHelper
    {
        public static int TotalPhotoCount { get; private set; }

        private static readonly Timer Timer;

        /// <summary>
        /// Key为md5码
        /// </summary>
        private static ConcurrentDictionary<string, PhotoInfo> _contentCache;

        /// <summary>
        /// 定时处理上传数据的时间间隔
        /// </summary>
        private static readonly TimeSpan CacheHandleInterval = TimeSpan.FromSeconds(3);        

        static DbHelper()
        {
            _contentCache = new ConcurrentDictionary<string, PhotoInfo>();
            Timer = new Timer(TimerHandler,null, CacheHandleInterval, Timeout.InfiniteTimeSpan);

            SetCount();
        }

        public static void AddContent(PhotoInfo photoInfo)
        {
            if (string.IsNullOrEmpty(photoInfo?.Md5))
            {
                throw new ArgumentNullException(nameof(photoInfo));
            }

            if (_contentCache.ContainsKey(photoInfo.Md5))
            {
                DeleteContent(photoInfo);
            }
            else
            {
                _contentCache.TryAdd(photoInfo.Md5, photoInfo);
            }
        }

        private static void TimerHandler(object obj)
        {
            try
            {
                var contentCache = _contentCache.Values.ToList();
                _contentCache.Clear();

                if (contentCache.Any())
                {
                    var repetitions = new List<PhotoInfo>();

                    foreach (var photoInfo in contentCache)
                    {
                        using (
                            var con =
                                new MySqlConnection(
                                    WebConfigurationManager.ConnectionStrings["MySqlConnString"].ToString()))
                        {
                            var count =
                                con.Count<PhotoInfo>(
                                    statement =>
                                        statement.Where($"{nameof(PhotoInfo.Md5):C}=@Md5")
                                            .WithParameters(new {Md5 = photoInfo.Md5}));

                            if (count > 0)
                            {
                                repetitions.Add(photoInfo);
                            }
                        }
                    }

                    //删除重复
                    foreach (var photoInfo in repetitions)
                    {
                        contentCache.Remove(photoInfo);
                        DeleteContent(photoInfo);
                    }

                    using (var con = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MySqlConnString"].ToString()))
                    {
                        IDbTransaction transaction = null;

                        try
                        {
                            con.Open();
                            transaction = con.BeginTransaction();

                            foreach (var photoInfo in contentCache)
                            {
                                con.Insert(photoInfo);
                            }

                            transaction.Commit();
                        }
                        catch (Exception exception)
                        {
                            LogManager.GetCurrentClassLogger().Error(exception);
                            transaction.Rollback();
                        }
                        finally
                        {
                            con.Close();
                        }
                    }

                    SetCount();
                }
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex);
            }
            finally
            {
                Timer.Change(CacheHandleInterval, Timeout.InfiniteTimeSpan);
            }
        }

        private static void DeleteContent(PhotoInfo photoInfo)
        {
            Task.Run(() =>
            {
                LogManager.GetCurrentClassLogger().Info($"出现重复内容，即将删除{JsonConvert.SerializeObject(photoInfo)}");

                QiniuHelper.Delete(photoInfo);
            });
        }

        private static void SetCount()
        {            
            using (var con = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MySqlConnString"].ToString()))
            {
                TotalPhotoCount = con.Count<PhotoInfo>();
            }
        }
    }
}