using System;

namespace CRMFocus.Domain
{
    public class SuspectView
    {
        public string SuspectId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string LastPurchaseUnit { get; set; }
        public string Dealer { get; set; }
        public string LastSalesName { get; set; }
        public string CurrentSalesName { get; set; }
        public string ScenarioName { get; set; }
        public string CallLog { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public DateTime? LastReactivate { get; set; }
    }
    public class SuspectCustomerView
    {
        public string SuspectId { get; set; }
        public string Name { get; set; }
        public string Telepon { get; set; }
        public string Email { get; set; }
        public string PembelianTerakhir { get; set; }
        public string Note { get; set; }
        public string LastSalesName { get; set; }
        public string CurrentSalesName { get; set; }
        public string CabangBaru { get; set; }
    }
}
