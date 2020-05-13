using CRMFocus.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class Kelurahan : BaseClass
    {
        [Column(Order = 1)]
        public int Id { get; set; }

        [Key]
        public string KelurahanCode { get; set; }

        public string KelurahanName { get; set; }

        public int KecamatanId { get; set; }

        [ForeignKey("Kecamatan")]
        public string KecamatanCode { get; set; }

        public Kecamatan Kecamatan { get; set; }

        public int KabupatenId { get; set; }

        [ForeignKey("Kabupaten")]
        public string KabupatenCode { get; set; }

        public Kabupaten Kabupaten { get; set; }

        public int ProvinceID { get; set; }

        [ForeignKey("Province")]
        public string ProvinceCode { get; set; }

        public Province Province { get; set; }
    }
}
