using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.DataAccess.Implementations
{
    public class DealerRepository : BaseRepository<Dealer>, IDealerRepository
    {
        public DealerRepository(CRMFocusContext context)
            : base(context)
        {
        }
    }
}
