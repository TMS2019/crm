using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.DataAccess.Implementations
{
    public class UnityTypeMarketRepository : BaseRepository<UnityTypeMarket>, IUnityTypeMarketRepository
    {
        public UnityTypeMarketRepository(CRMFocusContext context)
            : base(context)
        {
        }
    }
}
