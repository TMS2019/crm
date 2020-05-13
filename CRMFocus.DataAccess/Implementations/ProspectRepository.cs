using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.DataAccess.Implementations
{
    public class ProspectRepository : BaseRepository<Prospect>, IProspectRepository
    {
        public ProspectRepository(CRMFocusContext context) 
            : base(context)
        {
        }
    }
}
