using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using CRMFocus.Presentation.Models;
using Microsoft.Owin.Security;

[assembly: OwinStartupAttribute(typeof(CRMFocus.Presentation.Startup))]
namespace CRMFocus.Presentation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
