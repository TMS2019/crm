using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMFocus.Domain
{
    public class ScenarioApprovalDetailView : TambahDetailScenarioView
    {
        public string TargetCustomerName { get; set; }
        public string ODataQueryScript { get; set; }
        public string ScenarioCode { get; set; }
        public bool HasHistories { get; set; }
        public ScenarioHistoryView ScenarioHistoryView { get; set; }
    }
}
