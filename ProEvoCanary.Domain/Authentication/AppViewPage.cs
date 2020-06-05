using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;

namespace ProEvoCanary.Domain.Authentication
{
	public abstract class AppViewPage<TModel> : RazorPage<TModel>
    {
	    private readonly HttpContext _context;

	    public AppViewPage(HttpContext context)
	    {
		    _context = context;
	    }
        protected UserClaimsPrincipal CurrentUser
        {
            get
            {
                return new UserClaimsPrincipal(_context.User);
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
	    protected AppViewPage(HttpContext context) : base(context)
	    {
	    }
    }
}