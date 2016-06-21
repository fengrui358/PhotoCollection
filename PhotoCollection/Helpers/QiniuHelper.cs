using System;
using System.Web.Configuration;
using Newtonsoft.Json;
using NLog;
using PhotoCollection.Models;
using Qiniu.RPC;
using Qiniu.RS;

namespace PhotoCollection.Helpers
{
    public class QiniuHelper
    {
        private static string Bucket => WebConfigurationManager.AppSettings["qiniu-bucket"];

        static QiniuHelper()
        {
            Qiniu.Conf.Config.ACCESS_KEY = WebConfigurationManager.AppSettings["qiniu-ak"];
            Qiniu.Conf.Config.SECRET_KEY = WebConfigurationManager.AppSettings["qiniu-sk"];
        }

        public static string GetToken()
        {
            try
            {
                var put = new PutPolicy(Bucket, 7200);

                //调用Token()方法生成上传的Token
                return put.Token();
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex);
                return null;
            }
        }

        public static bool Delete(PhotoInfo photoInfo)
        {
            //实例化一个RSClient对象，用于操作BucketManager里面的方法
            RSClient client = new RSClient();
            CallRet ret = client.Delete(new EntryPath(Bucket, GetKeyFromUrl(photoInfo.Url)));
            if (ret.OK)
            {
                LogManager.GetCurrentClassLogger().Info($"删除照片成功，目标文件：{JsonConvert.SerializeObject(photoInfo)}");
                return true;
            }
            else
            {
                LogManager.GetCurrentClassLogger()
                    .Error(
                        $"删除文件失败，目标文件：{JsonConvert.SerializeObject(photoInfo)}，失败描述：{JsonConvert.SerializeObject(ret)}");

                return false;
            }
        }

        private static string GetKeyFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            var splitIndex = url.LastIndexOf("/", StringComparison.Ordinal);
            return url.Substring(splitIndex + 1);
        }
    }
}