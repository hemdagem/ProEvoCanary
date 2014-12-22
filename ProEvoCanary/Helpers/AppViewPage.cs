using System.Security.Claims;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProEvoCanary.Helpers
{
    public abstract class AppViewPage<TModel> : WebViewPage<TModel>
    {
        protected AppUser CurrentUser
        {
            get
            {
                return new AppUser(this.User as ClaimsPrincipal);
            }
        }

        protected string IsActive(string action, string controller, string area = "")
        {
            bool isCorrectAction = ViewContext.RouteData.Values["Action"].ToString() == action;
            bool isCorrectController = ViewContext.RouteData.Values["Controller"].ToString() == controller;
            bool isCorrectArea = true;
            if (!string.IsNullOrEmpty(area))
            {
                var routeValueDictionaryArea = ViewContext.RouteData.DataTokens["area"];
                isCorrectArea = routeValueDictionaryArea != null && routeValueDictionaryArea.ToString() == area;
            }

            return isCorrectAction && isCorrectController && isCorrectArea ? "active" : "";

        }

    }

    public abstract class AppViewPage : AppViewPage<dynamic>
    {
    }
}