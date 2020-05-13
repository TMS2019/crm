using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.DataAccess.Implementations
{
    public class ScenarioLeadMappingRepository : BaseRepository<ScenarioLeadMapping>, IScenarioLeadMappingRepository
    {
        public ScenarioLeadMappingRepository(CRMFocusContext context)
            : base(context)
        {
        }
    }
}
