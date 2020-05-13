using System.Web.Mvc;
using CRMFocus.Business.Interfaces;
using CRMFocus.Domain;
using System.Collections.Generic;

namespace CRMFocus.Presentation.Controllers
{
    public class SuspectToProspectController : Controller
    {
        private readonly ISuspectService _suspectService;

        public SuspectToProspectController(ISuspectService suspectService)
        {
            _suspectService = suspectService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read()
        {
            var data = _suspectService.GetAllSuspects();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeactivateSuspect(string[] suspectIds)
        {
            _suspectService.DeactivateSuspect(suspectIds);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteSuspect(string[] suspectIds)
        {
            _suspectService.DeleteSuspect(suspectIds);
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateStatus(string suspectID, int callStatus, string nextFollowup, string note)
        {
            _suspectService.UpdateStatus(suspectID, callStatus, nextFollowup, note);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCallScripts(string scenarioCode)
        {
            //[
            //    {
            //        "TipePertanyaan": 0,
            //        "Pertanyaan": "Are you good",
            //        "Jawabans": [
            //            {
            //                "Text": "Good"
            //            },
            //            {
            //                "Text": "Bad"
            //            },
            //            {
            //                "Text": "So-so"
            //            },
            //            {
            //                "Text": "Good"
            //            },
            //            {
            //                "Text": "Bad"
            //            },
            //            {
            //                "Text": "So-so"
            //            },
            //            {
            //                "Text": "Good"
            //            },
            //            {
            //                "Text": "Bad"
            //            },
            //            {
            //                "Text": "So-so"
            //            }
            //        ]
            //    },
            //    {
            //        "TipePertanyaan": 0,
            //        "Pertanyaan": "Are you good",
            //        "Jawabans": [
            //            {
            //                "Text": "Good"
            //            },
            //            {
            //                "Text": "Bad"
            //            },
            //            {
            //                "Text": "So-so"
            //            },
            //            {
            //                "Text": "Good"
            //            },
            //            {
            //                "Text": "Bad"
            //            },
            //            {
            //                "Text": "So-so"
            //            },
            //            {
            //                "Text": "Good"
            //            },
            //            {
            //                "Text": "Bad"
            //            },
            //            {
            //                "Text": "So-so"
            //            }
            //        ]
            //    }
            //]
            var data = _suspectService.GetCallScripts(scenarioCode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDataCustomer(string suspectID)
        {
            var data = _suspectService.GetDataCustomer(suspectID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SubmitAnswer(AnswerOfScriptView asv)
        {
            //{"asv":{"CustomerCode":1,"Qas":[{"TipePertanyaan":1,"Pertanyaan":"What is your age?","Jawabans":[{"Text":"Mau tau aja"}],"ScriptCode":160},{"TipePertanyaan":3,"Pertanyaan":"What wheel color combination do you prefer?","Jawabans":[{"Text":"Magenta", "Value":167},{"Text":"Black", "Value":168}],"ScriptCode":162}]}}
            _suspectService.SubmitAnswer(asv);
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}