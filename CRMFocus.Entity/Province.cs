using CRMFocus.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class Province : BaseClass
    {
        [Column(Order = 1)]
        public int Id { get; set; }

        [Key]
        public string ProvinceCode { get; set; }

        public string ProvinceName { get; set; }
    }
}
