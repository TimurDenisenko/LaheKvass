using LaheKvass.Models;
using System.Web.Mvc;

namespace LaheKvass
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            if (System.Diagnostics.Debugger.IsAttached)
                filters.Add(new UserState());
        }
    }
}
