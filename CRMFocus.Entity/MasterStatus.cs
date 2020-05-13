using CRMFocus.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class MasterStatus : BaseClass
    {
        [Column(Order = 1)]
        public int Id { get; set; }

        [Key]
        public int MasterStatusID { get; set; }

        [StringLength(30)]
        public string StatusGroup { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        public byte Value { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public byte IsActive { get; set; }
    }
}
