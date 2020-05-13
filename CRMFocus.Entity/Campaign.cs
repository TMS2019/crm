using CRMFocus.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class Campaign : BaseClass
    {
        [Key]
        [Column(Order = 1)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }
    }
}
