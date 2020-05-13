using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.DataAccess.Implementations
{
    public class LeadsUnitTransactionTemporaryRepository : BaseRepository<LeadsUnitTransactionTemporary>, ILeadsUnitTransactionTemporaryRepository
    {
        public LeadsUnitTransactionTemporaryRepository(CRMFocusContext context)
            : base(context)
        {
        }
    }
}
