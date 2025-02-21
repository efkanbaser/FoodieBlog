using FoodieBlog.Model.Entity;
using FoodieBlog.Model.Statics;
using FoodieBlog.MVCCoreUI.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FoodieBlog.MVCCoreUI.Filters
{
    public class UserFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            User user = context.HttpContext.Session.GetObject<User>(SessionKeys.ActiveUser);
            if (user == null)
            {
                context.Result = new RedirectResult("/Account/Index");
            }

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

    }
}
