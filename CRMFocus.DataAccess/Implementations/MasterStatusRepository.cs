using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.DataAccess.Implementations
{
    public class MasterStatusRepository : BaseRepository<MasterStatus>, IMasterStatusRepository
    {
        public MasterStatusRepository(CRMFocusContext context)
            : base(context)
        {
        }
    }
}
