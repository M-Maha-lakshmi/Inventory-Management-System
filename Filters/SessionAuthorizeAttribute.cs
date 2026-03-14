using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace InventoryManagementSystem.Filters
{
    public class SessionAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var username = context.HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(username))
            {
                var factory = context.HttpContext.RequestServices
                    .GetService(typeof(ITempDataDictionaryFactory)) as ITempDataDictionaryFactory;

                var tempData = factory.GetTempData(context.HttpContext);

                tempData["Message"] = "Please login first!";

                context.Result = new RedirectToActionResult("Login", "Account", null);
            }

            base.OnActionExecuting(context);
        }
    }
}