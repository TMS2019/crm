using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.DataAccess.Implementations
{
    public class SMSFollowupRepository : BaseRepository<SMSFollowup>, ISMSFollowupRepository
    {
        public SMSFollowupRepository(CRMFocusContext context)
            : base(context)
        {
        }
    }
}
