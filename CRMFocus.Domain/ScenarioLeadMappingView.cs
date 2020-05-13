using CRMFocus.Common;
using System;

namespace CRMFocus.Domain
{
    public class ScenarioLeadMappingView : BaseMessage
    {
        public string ScenarioLeadMappingViewId { get; set; }
        public string Nama { get; set; }
        public string Telepon { get; set; }
        public string Unit { get; set; }
        public string Email { get; set; }
        public string Varian { get; set; }
        public string Alamat { get; set; }       
        public DateTime? TanggalDiUnggah { get; set; }
        public string Cabang { get; set; }
        public string CabangDescription { get; set; }
        public string EngineCode { get; set; }
        public string EngineNumber { get; set; }
    }
}
