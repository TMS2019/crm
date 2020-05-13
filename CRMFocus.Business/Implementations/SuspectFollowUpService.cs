using CRMFocus.Entity;
using CRMFocus.Business.Interfaces;
using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;

namespace CRMFocus.Business.Implementations
{
    public class SuspectFollowUpService : BaseService<SuspectFollowUp>, ISuspectFollowUpService
    {
        private readonly ISuspectFollowUpRepository _suspectFollowUpRepository;

        public SuspectFollowUpService(ISuspectFollowUpRepository suspectFollowUpRepository)
            : base(suspectFollowUpRepository)
        {
            _suspectFollowUpRepository = suspectFollowUpRepository;
        }
    }
}
