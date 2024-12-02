using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LaheKvass.Models
{
    public class UserState : ActionFilterAttribute
    {   

        private string _requiredRole;
        public UserState()
        {
            _requiredRole = null;
        }
        public UserState(string requiredRole = "User")
        {
            _requiredRole = requiredRole;
        }
        public static string GetFullName()
        {
            AccountModel currentUser = GetCurrentUser();
            return $"{currentUser.FirstName} {currentUser.LastName}";
        }

        public static bool IsAdmin() =>
            GetCurrentUser().Role.Equals("Admin");
        public static AccountModel GetCurrentUser() =>
            HttpContext.Current.Cache["currentUser"] as AccountModel;
        public static void SetCurrentUser(AccountModel user) =>
            HttpContext.Current.Cache.Insert("currentUser", user);
        public static bool IsAuthorized() =>
            Convert.ToBoolean(HttpContext.Current.Cache["authorized"]);
        public static void IsAuthorized(bool isAuth) =>
            HttpContext.Current.Cache.Insert("authorized", isAuth);
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!IsAuthorized()
                && filterContext.ActionDescriptor.ActionName != "Register"
                && filterContext.ActionDescriptor.ActionName != "Login")
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Register" }));
                return;
            }
            AccountModel currentUser = GetCurrentUser();
            if (currentUser != null && currentUser.Role != null && _requiredRole != null && currentUser.Role != _requiredRole)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Drink", action = "Store" }));
                return;
            }
        }
    }
}