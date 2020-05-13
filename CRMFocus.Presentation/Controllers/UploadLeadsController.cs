using CRMFocus.Business.Interfaces;
using CRMFocus.Common;
using CRMFocus.Presentation.Resolver;
using System.Web;
using System.Web.Mvc;

namespace CRMFocus.Presentation.Controllers
{
    [CRMFocusFilter(Roles = "HO, Main Dealer, Dealer")]
    public class UploadLeadsController : Controller
    {
        private readonly UserRoleResolver _userRole;
        private readonly IUploadLeadsService _uploadLeadsService;

        public UploadLeadsController(IUploadLeadsService uploadLeadsService, UserRoleResolver userRole)
        {
            _userRole = userRole;
            _uploadLeadsService = uploadLeadsService;
        }

        public ActionResult Index()
        {
            ViewBag.UserRole = _userRole.GetThisUserRole();
            return View();
        }

        [HttpPost]
        public ActionResult PreviewExcell()
        {
            HttpFileCollectionBase files = Request.Files;
            var result = _uploadLeadsService.GetPreviewExcell(files, _userRole.GetThisUserRole());

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Save(string[] customerCodes)
        {
            if (customerCodes[0] != "")
            {
               var model = _uploadLeadsService.SaveExcell(customerCodes, _userRole.GetThisUserRole());
                return Json(new BaseMessage() { Message = model, Status = Utilities.SucceedStatus }, 
                    JsonRequestBehavior.AllowGet);
            }
          
            return Json(new BaseMessage() { Message = Utilities.SaveInComplete, Status = Utilities.ErrorStatus },
                JsonRequestBehavior.AllowGet);
        }
    }
}