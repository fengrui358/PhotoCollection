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
using PhotoCollection.Helpers;
using PhotoCollection.Models;
using Qiniu.RS;

namespace PhotoCollection.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.TotalCount = DbHelper.TotalPhotoCount;
            return View();
        }

        public ActionResult GetToken()
        {
            try
            {
                return Json(new {uptoken = QiniuHelper.GetToken()}, JsonRequestBehavior.AllowGet);
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
            DbHelper.AddContent(photoInfo);

            return null;
        }

        public ActionResult GetTotalCount()
        {
            return Content(DbHelper.TotalPhotoCount.ToString());
        }
    }
}