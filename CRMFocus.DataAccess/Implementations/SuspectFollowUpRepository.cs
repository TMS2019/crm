using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.DataAccess.Implementations
{
    public class SuspectFollowUpRepository : BaseRepository<SuspectFollowUp>, ISuspectFollowUpRepository
    {
        public SuspectFollowUpRepository(CRMFocusContext context)
            : base(context)
        {
        }
    }
}
