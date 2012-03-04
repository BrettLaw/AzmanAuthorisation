﻿using System.Web.Mvc;
using Lockdown.MVC.ActionResults;
using Lockdown.MVC.Client;
using Lockdown.MVC.Config;

namespace Lockdown.MVC.Filters
{
    public class AuthorisationFilter : ActionFilterAttribute
    {
        private readonly string _appName;
        private readonly string _stripPrefix;

        public AuthorisationFilter(string appName, string stripPrefix)
        {
            _appName = appName;
            _stripPrefix = stripPrefix;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var t = (ReflectedActionDescriptor)filterContext.ActionDescriptor;
            var m = t.MethodInfo;

            var opName = ConfigureFluent.GetOpName(m, _stripPrefix);

            var tokenStore = TokenStore.Current(_appName);
            var authorised = tokenStore.IsAuthorized(opName);

            if (authorised)
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            filterContext.Result = new HttpForbiddenResult();
        }
    }
}