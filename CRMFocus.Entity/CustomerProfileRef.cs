using CRMFocus.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class CustomerProfileRef : BaseClass
    {
        [Key]
        [Column(Order = 1)]
        public int Id { get; set; }

        public short? RowStatus { get; set; }

        [StringLength(30)]
        public string Type { get; set; }

        [StringLength(20)]
        public string Value { get; set; }

        [StringLength(200)]
        public string Text { get; set; }
    }
}
