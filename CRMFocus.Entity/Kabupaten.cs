using CRMFocus.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class Kabupaten : BaseClass
    {
        [Column(Order = 1)]
        public int Id { get; set; }

        [Key]
        public string KabupatenCode { get; set; }

        public string KabupatenName { get; set; }

        public int ProvinceID { get; set; }

        [ForeignKey("Province")]
        public string ProvinceCode { get; set; }

        public Province Province { get; set; }
    }
}
