using CRMFocus.Presentation.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Web.Mvc;

namespace CRMFocus.Presentation.Controllers
{
    public class RoleController : Controller
    {
        CRMFocusContext _context;

        public RoleController()
        {
            _context = new CRMFocusContext();
        }
        
        public ActionResult Index()
        {
            var role = _context.Roles.ToList();
            return View(role);
        }
  
        public ActionResult Create()
        {
            var role = new IdentityRole();
            return View(role);
        }
        
        [HttpPost]
        public ActionResult Create(IdentityRole role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            return View(_context.Roles.FirstOrDefault(f => f.Id == id));
        }

        [HttpPost]
        public ActionResult Delete(IdentityRole role)
        {
            var result = _context.Roles.FirstOrDefault(f => f.Id == role.Id);
            _context.Roles.Remove(result);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //public ActionResult Manage()
        //{
        //    var role = _context.Roles.ToList();
        //    return View(role);
        //}
    }
}