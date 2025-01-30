using FoodieBlog.Model.Entity;
using FoodieBlog.Model.Statics;
using FoodieBlog.MVCCoreUI.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FoodieBlog.MVCCoreUI.Filters
{
    public class AdminFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            User user = context.HttpContext.Session.GetObject<User>(SessionKeys.ActiveAdmin);
            if (user == null)
            {
                context.Result = new RedirectResult("/AdminPanel/Admin/Login");
            }

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

    }
}
