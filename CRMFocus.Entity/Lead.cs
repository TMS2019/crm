using CRMFocus.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class Lead : BaseClass
    {
        [Column(Order = 1)]
        public int Id { get; set; }

        [Timestamp]
        [MaxLength(8)]
        public byte[] TimeStatus { get; set; }

        public int RowStatus { get; set; }

        public DateTime SourceSystemCreatedTime { get; set; }

        [StringLength(50)]
        public string SourceSystemCreatedBy { get; set; }

        public DateTime SourceSystemLastModifiedTime { get; set; }

        [StringLength(50)]
        public string SourceSystemLastModifiedBy { get; set; }

        [Key]
        public int CRMCustomerCode { get; set; }

        [StringLength(50)]
        public string IDCardNo { get; set; }

        [StringLength(1)]
        public string CustomerCode { get; set; }

        public DateTime BirthDate { get; set; }

        [StringLength(150)]
        public string Address { get; set; }

        [StringLength(1)]
        public string Gender { get; set; }

        [StringLength(1)]
        public string Religion { get; set; }

        [StringLength(2)]
        public string Profesion { get; set; }

        [StringLength(2)]
        public string Spending { get; set; }

        [StringLength(2)]
        public string Education { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(25)]
        public string CellNo { get; set; }

        [StringLength(1)]
        public string isCallable { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(10)]
        public string LastDealerName { get; set; }

        [StringLength(10)]
        public string LastSalesNo { get; set; }

        [StringLength(50)]
        public string LastSalesName { get; set; }

        [StringLength(10)]
        public string CurrentDealer { get; set; }

        [StringLength(10)]
        public string CurrentSalesNo { get; set; }

        [StringLength(50)]
        public string CurrentSalesName { get; set; }

        public List<LeadsUnitTransaction> LeadsUnitTransactions { get; set; }
    }
}
