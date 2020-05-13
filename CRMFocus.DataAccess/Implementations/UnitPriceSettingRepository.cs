using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.DataAccess.Implementations
{
    public class UnitPriceSettingRepository : BaseRepository<UnitPriceSetting>, IUnitPriceSettingRepository
    {
        public UnitPriceSettingRepository(CRMFocusContext context)
            : base(context)
        {
        }
    }
}
