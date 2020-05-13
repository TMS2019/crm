using CRMFocus.Business.Implementations;
using CRMFocus.Business.Interfaces;
using CRMFocus.DataAccess.Implementations;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Presentation.Resolver;
using Microsoft.Practices.Unity;
using System.Web.Http;

namespace CRMFocus.Presentation
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();

            //put all interface repository and implementation here, if you using web api controller
            container.RegisterType<IDummyRepository, DummyRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ILeadsTemporaryRepository, LeadsTemporaryRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ILeadsUnitTransactionTemporaryRepository, LeadsUnitTransactionTemporaryRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IProspectTemporaryRepository, ProspectTemporaryRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISuspectTemporaryRepository, SuspectTemporaryRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISuspectRepository, SuspectRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ILeadsRepository, LeadsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ILeadsUnitTransactionRepository, LeadsUnitTransactionRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IScenarioRepository, ScenarioRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IMasterStatusRepository, MasterStatusRepository>(new HierarchicalLifetimeManager());           
            container.RegisterType<IDealerRepository, DealerRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISuspectFollowUpRepository, SuspectFollowUpRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ICustomerProfileRefRepository, CustomerProfileRefRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IMainDealerRepository, MainDealerRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IScenarioSettingRepository, ScenarioSettingRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitPriceSettingRepository, UnitPriceSettingRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IScenarioLeadMappingRepository, ScenarioLeadMappingRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IScenarioFilterRepository, ScenarioFilterRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IProvinceRepository, ProvinceRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IKabupatenRepository, KabupatenRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IKecamatanRepository, KecamatanRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IKelurahanRepository, KelurahanRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISMSFollowupRepository, SMSFollowupRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnityTypeMarketRepository, UnityTypeMarketRepository>(new HierarchicalLifetimeManager());

            //put all interface service and implementation here, if you using web api controller
            container.RegisterType<IDummyService, DummyService>(new HierarchicalLifetimeManager());
            container.RegisterType<IUploadLeadsService, UploadLeadsService>(new HierarchicalLifetimeManager());
            container.RegisterType<IInactiveLeadsService, InactiveLeadsService>(new HierarchicalLifetimeManager());
            container.RegisterType<IManageLeadsService, ManageLeadsService>(new HierarchicalLifetimeManager());
            container.RegisterType<ISuspectService, SuspectService>(new HierarchicalLifetimeManager());
            container.RegisterType<ISuspectFollowUpService, SuspectFollowUpService>(new HierarchicalLifetimeManager());
            container.RegisterType<IScenarioSettingService, ScenarioSettingService>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitPriceSettingService, UnitPriceSettingService>(new HierarchicalLifetimeManager());
            container.RegisterType<IManageSendSmsService, ManageSendSmsService>(new HierarchicalLifetimeManager());
            container.RegisterType<IScenarioService, ScenarioService>(new HierarchicalLifetimeManager());
            container.RegisterType<IFollowUpBySmsService, FollowUpBySmsService>(new HierarchicalLifetimeManager());


            config.DependencyResolver = new UnityResolver(container);

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
