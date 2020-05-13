using System;

namespace CRMFocus.Domain
{
    public class FollowUpBySmsDetailsView
    {
        public int CRMCustomerNum { get; set; }
        public string Nama { get; set; }
        public string Telepon { get; set; }
        public string NoPlat { get; set; }
        public DateTime? TglUpload { get; set; }
        public DateTime? TglTerkirim { get; set; }
        public string Scenario { get; set; }
        public string Status { get; set; }
        public string Unit { get; set; }
    }
}
