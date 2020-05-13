using CRMFocus.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class Dealer : BaseClass
    {
        [Column(Order = 1)]
        public int? Id { get; set; }

        public int? MainDealerId { get; set; }

        [StringLength(4)]
        public string MainDealerCode { get; set; }

        [Key]
        [StringLength(10)]
        public string DealerCode { get; set; }

        [StringLength(12)]
        public string SAPDealerCode { get; set; }

        [StringLength(20)]
        public string CustomerCode { get; set; }

        [StringLength(20)]
        public string VendorCode { get; set; }

        public int? DealerStatusUOMID { get; set; }

        [StringLength(20)]
        public string DealerStatusCode { get; set; }

        public int DealerGroupID { get; set; }

        [StringLength(20)]
        public string DealerGroupCode { get; set; }

        public DateTime? EstablishmentDate { get; set; }

        public int? DealerWorkingStatusUOMID { get; set; }

        [StringLength(30)]
        public string DealerWorkingStatus { get; set; }

        [StringLength(50)]
        public string Reference { get; set; }

        [StringLength(70)]
        public string DealerName { get; set; }

        [StringLength(70)]
        public string BrandCode { get; set; }

        [StringLength(150)]
        public string Address { get; set; }

        public int KabupatenID { get; set; }

        [StringLength(50)]
        public string KabupatenCode { get; set; }

        [StringLength(50)]
        public string KabupatenName { get; set; }

        public int AreaID { get; set; }

        [StringLength(50)]
        public string Telp1 { get; set; }

        [StringLength(50)]
        public string Telp2 { get; set; }

        [StringLength(50)]
        public string HP { get; set; }

        [StringLength(50)]
        public string Fax { get; set; }

        public byte isActive { get; set; }
    }
}
