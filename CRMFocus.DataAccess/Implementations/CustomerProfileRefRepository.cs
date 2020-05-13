using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.DataAccess.Implementations
{
    public class CustomerProfileRefRepository : BaseRepository<CustomerProfileRef>, ICustomerProfileRefRepository
    {
        public CustomerProfileRefRepository(CRMFocusContext context)
            : base(context)
        {
        }
    }
}
