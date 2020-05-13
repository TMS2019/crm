using System;

namespace CRMFocus.Domain
{
    public class FollowUpBySmsView
    {
        public string ScenarioCode { get; set; }
        public string NamaSkenario { get; set; }
        public string TipeSkenario { get; set; }
        public DateTime? TanggalMulai { get; set; }
        public DateTime? TanggalSelesai { get; set; }
        public int Total { get; set; }
        public int Terkirim { get; set; }
        public int Gagal { get; set; }
    }
}
