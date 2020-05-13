using CRMFocus.Common;
using System;

namespace CRMFocus.Domain
{
    public class UploadLeadsView : BaseMessage
    {
        public string UploadLeadsViewId { get; set; }
        public string CustomerCode { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string GenderDescription { get; set; }
        public string Religion { get; set; }
        public string ReligionDescription { get; set; }
        public string Profesion { get; set; }
        public string ProfesionDescription { get; set; }
        public string Spending { get; set; }
        public string SpendingDescription { get; set; }
        public string Education { get; set; }
        public string EducationDescription { get; set; }
        public string Name { get; set; }
        public string CellNo { get; set; }
        public string isCallable { get; set; }
        public string Email { get; set; }

        public string SourceData { get; set; }

        public string UnitMarketName { get; set; }
        public string EngineCode { get; set; }
        public string EngineNo { get; set; }
        public DateTime? TglBeli { get; set; }
        public string PaymentType { get; set; }
        public string PaymentTypeDescription { get; set; }
        public string ServiceType { get; set; }
        public string ServiceTypeDescription { get; set; }
        public DateTime? ServiceDate { get; set; }
        public string Kelurahan { get; set; }
        public string Kecamatan { get; set; }
        public string Kabupaten { get; set; }
        public string Provinsi { get; set; }
        public string UnitTypeSegment { get; set; }
        public string UnitTypeSeries { get; set; }

        public string MainDealerCode { get; set; }
        public string MainDealerName { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
    }
}
