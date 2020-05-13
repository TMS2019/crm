using CRMFocus.Presentation.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMFocus.Presentation.Resolver
{
    public class CRMFocusFilterAttribute : AuthorizeAttribute
    {
        public CRMFocusFilterAttribute()
        {
            View = "AuthorizeFailed";
        }

        public string View { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            IsUserAuthorized(filterContext);
        }

        private void IsUserAuthorized(AuthorizationContext filterContext)
        {
            // If the Result returns true then the user is Authorized 
            if (isAuthorizedUser(filterContext))
                return;

            //If the user is Un-Authorized then Navigate to Auth Failed View 
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var vr = new ViewResult();
                vr.ViewName = View;

                ViewDataDictionary dict = new ViewDataDictionary();
                dict.Add("Message", "Sorry you are not Authorized to Perform this Action");

                vr.ViewData = dict;

                var result = vr;

                filterContext.Result = result;
            }
        }

        private bool isAuthorizedUser(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
                    .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                var context = new CRMFocusContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var userRoles = UserManager.GetRoles(user.Id);
                var actionRoles = this.Roles.Trim().ToLower().Split(',');

                if (userRoles.Count > 0)
                {
                    // return true if the user roles inside in action roles
                    return actionRoles.Where(w => w.Contains(userRoles[0].ToLower())).Count() > 0 ? true : false;                    
                }
            }

            return false;
        }
    }
}