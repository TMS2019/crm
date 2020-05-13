using CRMFocus.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class MainDealer : BaseClass
    {
        [Column(Order = 1)]
        public int Id { get; set; }

        [Key]
        [StringLength(50)]
        public string MainDealerCode { get; set; }

        [StringLength(50)]
        public string MainDealerName { get; set; }

        [StringLength(50)]
        public string MainDealerInitial { get; set; }

        [StringLength(50)]
        public string MainDealerDescription { get; set; }

        [StringLength(50)]
        public string MainDealerDelimiter { get; set; }

        [StringLength(5)]
        public string DPAmount { get; set; }

        public float DPDate { get; set; }

        public DateTime? DPUpdatedUserName { get; set; }

        [StringLength(10)]
        public string RegionCode { get; set; }

        [StringLength(4)]
        public string isHSO { get; set; }

        public byte? SAPAHMJournalAccountCode { get; set; }

        [StringLength(50)]
        public string SAPHSOCustomerCode { get; set; }

        [StringLength(100)]
        public string Address { get; set; }


        [StringLength(100)]
        public string KabupatenID { get; set; }

        [StringLength(100)]
        public string BankAccountName { get; set; }

        [StringLength(50)]
        public string BankName { get; set; }

        [StringLength(50)]
        public string BankAccountNumber { get; set; }

        [StringLength(50)]
        public string Kelurahan { get; set; }

        [StringLength(50)]
        public string SupervisorName { get; set; }

        [StringLength(50)]
        public string Sequence { get; set; }

        [StringLength(50)]
        public string TimeStatus { get; set; }

        [Timestamp]
        [MaxLength(8)]
        public byte[] ETLBatchRunID { get; set; }

        public int Ring1 { get; set; }

        public int Ring2 { get; set; }

        public int Ring3 { get; set; }

        public int RingOthers { get; set; }

        public int? KabupatenCode { get; set; }

        [StringLength(50)]
        public string Rowstatus { get; set; }

        public short ItemNo { get; set; }

        public int? Latitude { get; set; }

        public float? Longitude { get; set; }

        public float? kecamatanID { get; set; }

        public int? KecamatanCode { get; set; }
    }
}
