using CRMFocus.Presentation.Resolver;
using System.Web.Mvc;

namespace CRMFocus.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //[CRMFocusFilter(Roles = "Main Dealer")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //[CRMFocusFilter(Roles = "Dealer, HO")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}