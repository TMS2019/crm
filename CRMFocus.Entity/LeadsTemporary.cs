using CRMFocus.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class LeadsTemporary : BaseClass
    {
        [Key]
        [Column(Order = 1)]
        public int Id { get; set; }
        
        public string CustomerCode { get; set; }

        [StringLength(50)]
        public string IDCardNo { get; set; }

        public DateTime? BirthDate { get; set; }

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
    }
}
