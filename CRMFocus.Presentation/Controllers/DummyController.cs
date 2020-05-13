using CRMFocus.Business.Implementations;
using CRMFocus.Presentation.Resolver;
using System.Web.Mvc;

namespace CRMFocus.Presentation.Controllers
{
    public class DummyController : Controller
    {
        private readonly UserRoleResolver _userRole;

        public DummyController(UserRoleResolver user)
        {
            _userRole = user;
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}
