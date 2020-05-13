using CRMFocus.Business.Interfaces;
using CRMFocus.Entity;
using CRMFocus.Presentation.Resolver;
using System.Collections.Generic;
using System.Web.Http;

namespace CRMFocus.Presentation.Controllers.Api
{
    public class DummyController : ApiController
    {
        private readonly UserRoleResolver _userRole;
        private readonly IDummyService _dummyService;

        public DummyController(IDummyService dummyService, UserRoleResolver userRole)
        {
            _userRole = userRole;
            _dummyService = dummyService;
        }

        [HttpGet]
        public IEnumerable<Dummy> Get()
        {
            var role = _userRole.GetThisUserRole();
            return _dummyService.Find(f => f.IsDeleted == false);
        }

        [HttpGet]
        public Dummy Get(int id)
        {
            var role = _userRole.GetThisUserRole();
            return _dummyService.GetById(id);
        }

        [HttpPost]
        public void Create(Dummy dummy)
        {
            var role = _userRole.GetThisUserRole();
            dummy.UserRole = role;
            _dummyService.Create(dummy);
        }

        [HttpPut]
        public void Update(Dummy dummy)
        {
            var role = _userRole.GetThisUserRole();
            dummy.UserRole = role;
            _dummyService.Update(dummy.Id, dummy);
        }

        [HttpDelete]
        public void Delete(Dummy dummy)
        {
            var role = _userRole.GetThisUserRole();
            dummy.UserRole = role;
            _dummyService.Delete(dummy.Id);
        }
    }
}