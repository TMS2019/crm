using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.DataAccess.Implementations
{
    public class LeadsTemporaryRepository : BaseRepository<LeadsTemporary>, ILeadsTemporaryRepository
    {
        public LeadsTemporaryRepository(CRMFocusContext context)
            : base(context)
        {
        }
    }
}
