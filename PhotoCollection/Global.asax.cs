using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Dapper.FastCrud;
using NLog;

namespace PhotoCollection
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Error += OnError;

            OrmConfiguration.DefaultDialect = SqlDialect.MySql;

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        /// <summary>
        /// 未处理的异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void OnError(object sender, EventArgs eventArgs)
        {
            //todo:改成更通用的module捕获异常的方法 http://www.cnblogs.com/youring2/archive/2012/04/25/2469974.html
            //获取到HttpUnhandledException异常，这个异常包含一个实际出现的异常
            Exception ex = Server.GetLastError();
            LogManager.GetCurrentClassLogger().Error(ex);
        }
    }
}
