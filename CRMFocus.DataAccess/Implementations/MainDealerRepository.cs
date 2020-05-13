using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.DataAccess.Implementations
{
    public class MainDealerRepository : BaseRepository<MainDealer>, IMainDealerRepository
    {
        public MainDealerRepository(CRMFocusContext context)
            : base(context)
        {
        }
    }
}
