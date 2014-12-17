using System.Web;
using System.Web.Mvc;

using taskman.Models.exception;

namespace taskman
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
			filters.Clear();
            //filters.Add(new HandlerAttribute(), 0);
        }
    }
}