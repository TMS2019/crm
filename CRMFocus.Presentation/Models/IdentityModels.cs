using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using CRMFocus.Entity;

namespace CRMFocus.Presentation.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class CRMFocusContext : IdentityDbContext<ApplicationUser>
    {
        public CRMFocusContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        // Put the domain dto here
        public DbSet<Dummy> Dummies { get; set; }
        public DbSet<LeadsTemporary> LeadsTemporaries { get; set; }
        public DbSet<LeadsUnitTransactionTemporary> LeadsUnitTransactionTemporaries { get; set; }
        public DbSet<SuspectTemporary> SuspectTemporaries { get; set; }
        public DbSet<ProspectTemporary> ProspectTemporaries { get; set; }
        public DbSet<Scenario> Scenarios { get; set; }
        public DbSet<Suspect> Suspects { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<LeadsUnitTransaction> LeadsUnitTransactions { get; set; }
        public DbSet<MasterStatus> MasterStatuses { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Kabupaten> Kabupatens { get; set; }
        public DbSet<Kecamatan> Kecamatans { get; set; }
        public DbSet<Kelurahan> Kelurahans { get; set; }
        public DbSet<Dealer> Dealers { get; set; }
        public DbSet<SuspectFollowUp> SuspectFollowUps { get; set; }
        public DbSet<CustomerProfileRef> CustomerProfileRefs { get; set; }
        public DbSet<MainDealer> MainDealers { get; set; }
        public DbSet<UnityTypeMarket> UnityTypeMarkets { get; set; }
        public DbSet<ScenarioSetting> ScenarioSettings { get; set; }
        public DbSet<UnitPriceSetting> UnitPriceSettings { get; set; }
        public DbSet<ScenarioLeadMapping> ScenarioLeadMappings { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ScenarioFilter> ScenarioFilters { get; set; }
        public DbSet<ScenarioHistory> ScenarioHistories { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Prospect> Prospects { get; set; }
        public DbSet<ProspectFollowUp> ProspectFollowUps { get; set; }
        public DbSet<SMSFollowup> SMSFollowups { get; set; }
        public DbSet<Script> Scripts { get; set; }
        public DbSet<ScenarioScriptMapping> ScenarioScriptMappings { get; set; }
        public DbSet<Answer> Answers { get; set; }


        public static CRMFocusContext Create()
        {
            return new CRMFocusContext();
        }
    }
}