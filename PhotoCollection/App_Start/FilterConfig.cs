using System.Web.Mvc;
using PhotoCollection.Filters;

namespace PhotoCollection
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new TrackPageLoadPerformanceAttribute());
        }
    }
}