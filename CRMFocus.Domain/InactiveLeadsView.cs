using System;

namespace CRMFocus.Domain
{
    public class InactiveLeadsView
    {
        public string SuspectId { get; set; }
        public string Name { get; set; }
        public string Telepon { get; set; }
        public string Email { get; set; }
        public string Kota { get; set; }
        public string Sumber { get; set; }
        public DateTime TglMasuk { get; set; }
        public string Cabang { get; set; }
        public string Scenario { get; set; }
        public string Stage { get; set; }
        public string Note { get; set; }
    }
}
