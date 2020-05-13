using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.DataAccess.Implementations
{
    public class SuspectRepository : BaseRepository<Suspect>, ISuspectRepository
    {
        public SuspectRepository(CRMFocusContext context)
            : base(context)
        {
        }
    }
}
