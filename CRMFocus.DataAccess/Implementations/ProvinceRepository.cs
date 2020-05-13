using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.DataAccess.Implementations
{
    public class ProvinceRepository : BaseRepository<Province>, IProvinceRepository
    {
        public ProvinceRepository(CRMFocusContext context)
            : base(context)
        {
        }
    }
}
