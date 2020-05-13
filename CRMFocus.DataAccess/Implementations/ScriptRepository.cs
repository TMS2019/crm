using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.DataAccess.Implementations
{
    public class ScriptRepository : BaseRepository<Script>, IScriptRepository
    {
        public ScriptRepository(CRMFocusContext context)
            : base(context)
        {
        }
    }
}
