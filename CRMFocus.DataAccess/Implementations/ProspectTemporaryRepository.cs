using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.DataAccess.Implementations
{
    public class ProspectTemporaryRepository : BaseRepository<ProspectTemporary>, IProspectTemporaryRepository
    {
        public ProspectTemporaryRepository(CRMFocusContext context)
            : base(context)
        {
        }
    }
}
