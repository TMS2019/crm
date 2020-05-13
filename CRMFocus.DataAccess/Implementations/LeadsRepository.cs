using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.DataAccess.Implementations
{
    public class LeadsRepository : BaseRepository<Lead>, ILeadsRepository
    {
        public LeadsRepository(CRMFocusContext context)
            : base(context)
        {
        }
    }
}
