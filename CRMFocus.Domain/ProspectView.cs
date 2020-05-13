using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMFocus.Domain
{
    public class ProspectView
    {
        public int ProspectId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string LastPurchaseUnit { get; set; }
        public string Dealer { get; set; }
        public string SalesName { get; set; }
        public string City { get; set; }
        public string CurrentDealer { get; set; }
        public string FollowUp { get; set; }
        public string Status { get; set; }
    }
}
