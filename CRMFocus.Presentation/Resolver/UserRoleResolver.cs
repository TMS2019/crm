using CRMFocus.Presentation.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Web;

namespace CRMFocus.Presentation.Resolver
{
    public class UserRoleResolver
    {
        public string GetThisUserRole()
        {
            var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
                     .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var context = new CRMFocusContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var userRoles = UserManager.GetRoles(user.Id);

            return userRoles[0].ToLower();
        }
    }
}