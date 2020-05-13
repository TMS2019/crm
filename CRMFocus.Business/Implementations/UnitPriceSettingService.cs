using System;
using CRMFocus.Entity;
using CRMFocus.Common;
using CRMFocus.Business.Interfaces;
using CRMFocus.DataAccess.Interfaces;

namespace CRMFocus.Business.Implementations
{
    public class UnitPriceSettingService : BaseService<UnitPriceSetting>, IUnitPriceSettingService
    {
        private readonly IUnitPriceSettingRepository _unitPriceSettingRepository;

        public UnitPriceSettingService(IUnitPriceSettingRepository unitPriceSettingRepository)
            : base(unitPriceSettingRepository)
        {
            _unitPriceSettingRepository = unitPriceSettingRepository;
        }
    }
}
