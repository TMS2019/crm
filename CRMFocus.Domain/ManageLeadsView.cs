using System;

namespace CRMFocus.Domain
{
    public class ManageLeadsView
    {
        public string SuspectId { get; set; }
        public string Name { get; set; }
        public string Telepon { get; set; }
        public string Email { get; set; }
        public string PembelianTerakhir { get; set; }
        public string Sumber { get; set; }
        public DateTime TglMasuk { get; set; }
        public string Cabang { get; set; }
        public string CabangBaru { get; set; }
        public string ScenarioName { get; set; }
    }
}
