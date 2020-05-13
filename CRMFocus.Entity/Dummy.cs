using CRMFocus.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class Dummy : BaseClass
    {
        [Column(Order = 1)]
        public int Id { get; set; }

        public string DummyName { get; set; }
    }
}
