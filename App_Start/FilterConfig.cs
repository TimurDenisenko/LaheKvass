using LaheKvass.Models;
using System.Web.Mvc;

namespace LaheKvass
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new UserState());
        }
    }
}
