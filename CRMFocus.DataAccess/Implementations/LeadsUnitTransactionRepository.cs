using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.DataAccess.Implementations
{
    public class LeadsUnitTransactionRepository : BaseRepository<LeadsUnitTransaction>, ILeadsUnitTransactionRepository
    {
        public LeadsUnitTransactionRepository(CRMFocusContext context)
            : base(context)
        {
        }
    }
}
