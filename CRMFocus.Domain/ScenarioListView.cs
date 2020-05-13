using System;

namespace CRMFocus.Domain
{
    public class ScenarioListView
    {
        public string ScenarioCode { get; set; }
        public string NamaScenario { get; set; }
        public string Deskripsi { get; set; }
        public DateTime? TglSubmit { get; set; }
        public string Requester { get; set; }
        public string Cabang { get; set; }
        public DateTime TglMulai { get; set; }
        public DateTime TglSelesai { get; set; }
        public string Status { get; set; }
        public bool isActive { get; set; }
        public string ScenarioHistoryRejectReason { get; set; }
    }
}
