using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMFocus.Domain
{
    public class ScenarioHistoryView
    {
        public string ScenarioCode { get; set; }
        public string RejectReason { get; set; }
        public bool IsApproved { get; set; }
    }
}
