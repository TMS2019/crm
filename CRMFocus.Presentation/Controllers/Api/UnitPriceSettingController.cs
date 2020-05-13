using CRMFocus.Business.Interfaces;
using CRMFocus.Entity;
using CRMFocus.Presentation.Resolver;
using System.Collections.Generic;
using System.Web.Http;

namespace CRMFocus.Presentation.Controllers.Api
{
    public class UnitPriceSettingController : ApiController
    {
        private readonly UserRoleResolver _userRole;
        private readonly IUnitPriceSettingService _unitPriceSettingService;

        public UnitPriceSettingController(IUnitPriceSettingService unitPriceSettingService, UserRoleResolver userRole)
        {
            _userRole = userRole;
            _unitPriceSettingService = unitPriceSettingService;
        }

        [HttpGet]
        public IEnumerable<UnitPriceSetting> Get()
        {
            var role = _userRole.GetThisUserRole();
            return _unitPriceSettingService.Find(f => f.IsDeleted == false);
        }

        [HttpGet]
        public UnitPriceSetting Get(int id)
        {
            var role = _userRole.GetThisUserRole();
            return _unitPriceSettingService.GetById(id);
        }

        [HttpPost]
        public void Create(UnitPriceSetting unitPriceSetting)
        {
            var role = _userRole.GetThisUserRole();
            unitPriceSetting.UserRole = role;
            _unitPriceSettingService.Create(unitPriceSetting);
        }

        [HttpPut]
        public void Update(UnitPriceSetting unitPriceSetting)
        {
            var role = _userRole.GetThisUserRole();
            unitPriceSetting.UserRole = role;
            _unitPriceSettingService.Update(unitPriceSetting.Id, unitPriceSetting);
        }

        [HttpDelete]
        public void Delete(UnitPriceSetting unitPriceSetting)
        {
            var role = _userRole.GetThisUserRole();
            unitPriceSetting.UserRole = role;
            _unitPriceSettingService.Delete(unitPriceSetting.Id);
        }
    }
}
