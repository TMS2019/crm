using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.DataAccess.Implementations
{
    public class AnsweRepository : BaseRepository<Answer>, IAnswerRepository
    {
        public AnsweRepository(CRMFocusContext context)
            : base(context)
        {
        }
    }
}
