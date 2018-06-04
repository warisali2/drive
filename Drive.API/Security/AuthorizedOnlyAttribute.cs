using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Drive.Security
{
    public class AuthorizedOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (SessionManager.IsValidUser == false)
                filterContext.Result = new RedirectResult("~/User/Login");

            base.OnActionExecuting(filterContext);
        }
    }
}