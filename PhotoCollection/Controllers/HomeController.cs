using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Dapper.FastCrud;
using MySql.Data.MySqlClient;
using NLog;
using PhotoCollection.Models;
using Qiniu.RS;

namespace PhotoCollection.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetToken()
        {
            try
            {
                var bucket = WebConfigurationManager.AppSettings["qiniu-bucket"];
                Qiniu.Conf.Config.ACCESS_KEY = WebConfigurationManager.AppSettings["qiniu-ak"];
                Qiniu.Conf.Config.SECRET_KEY = WebConfigurationManager.AppSettings["qiniu-sk"];

                var put = new PutPolicy(bucket, 7200);

                //调用Token()方法生成上传的Token
                string upToken = put.Token();

                return Json(new { uptoken = upToken }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex);
                return null;
            }
        }

        [HttpPost]
        public ActionResult AddContent(PhotoInfo photoInfo)
        {
            //todo:延迟批量写入
            using (var con = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MySqlConnString"].ToString()))
            {
                con.Insert(photoInfo);
            }

            return Content(string.Empty);
        }
    }
}