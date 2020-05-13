using CRMFocus.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class UnitPriceSetting : BaseClass
    {
        [Key]
        [Column(Order = 1)]
        public int Id { get; set; }

        public string Merk { get; set; }
        public string Varian { get; set; }
        public string StartPrice { get; set; }
        public string EndPrice { get; set; }
    }
}
