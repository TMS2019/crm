using System;

namespace CRMFocus.Domain
{
    public class FilterLainView
    {
        public string Section { get; set; }
        public DateTime? TanggalMulai { get; set; }
        public string Operator { get; set; }
        public string Jumlah { get; set; }
        public string DayOrMonth { get; set; }
        public string Logic { get; set; }
    }
}
