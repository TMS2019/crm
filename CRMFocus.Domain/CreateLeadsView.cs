using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMFocus.Domain
{
    public class CreateLeadsView
    {
        public int Id { get; set; }
        public string CustomerCode { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public string Profesion { get; set; }
        public string Spending { get; set; }
        public string Education { get; set; }
        public string Name { get; set; }
        public string CellNo { get; set; }
        public string isCallable { get; set; }
        public string Email { get; set; }
        public List<LeadsUnitTransactionView> LeadsUnitTransaction { get; set; }
    }

    public class LeadsUnitTransactionView
    {
        public int Id { get; set; }
        public string UnitMarketName { get; set; }
        public string EngineCode { get; set; }
        public string EngineNo { get; set; }
        public DateTime? TglBeli { get; set; }
        public string PaymentType { get; set; }
        public string ServiceType { get; set; }
        public string SourceData { get; set; }
        public DateTime? SeviceDate { get; set; }
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
        public int LeadsTemporaryId { get; set; }
    }
    public class LeadsTemporaryView
    {
        public int Id { get; set; }
        public string CustomerCode { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public string Profesion { get; set; }
        public string Spending { get; set; }
        public string Education { get; set; }
        public string Name { get; set; }
        public string CellNo { get; set; }
        public string isCallable { get; set; }
        public string Email { get; set; }
        public string UnitMarketName { get; set; }
        public string EngineCode { get; set; }
        public string EngineNo { get; set; }
        public DateTime? TglBeli { get; set; }
        public string PaymentType { get; set; }
        public string ServiceType { get; set; }
        public string SourceData { get; set; }
        public DateTime? SeviceDate { get; set; }
        public string Kelurahan { get; set; }
        public string Kecamatan { get; set; }
        public string Kabupaten { get; set; }
        public string Provinsi { get; set; }
        public string UnitTypeSegment { get; set; }
        public string UnitTypeSeries { get; set; }
    }
}
