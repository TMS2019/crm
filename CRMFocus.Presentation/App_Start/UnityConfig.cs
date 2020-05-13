using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using CRMFocus.Presentation.Controllers;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.DataAccess.Implementations;
using CRMFocus.Business.Interfaces;
using CRMFocus.Business.Implementations;

namespace CRMFocus.Presentation
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());

            // put all interface repository and implementation here, if you using mvc controller
            container.RegisterType<IDummyRepository, DummyRepository>();
            container.RegisterType<ILeadsTemporaryRepository, LeadsTemporaryRepository>();
            container.RegisterType<ILeadsUnitTransactionTemporaryRepository, LeadsUnitTransactionTemporaryRepository>();
            container.RegisterType<IProspectTemporaryRepository, ProspectTemporaryRepository>();
            container.RegisterType<ISuspectTemporaryRepository, SuspectTemporaryRepository>();
            container.RegisterType<ISuspectRepository, SuspectRepository>();
            container.RegisterType<ILeadsRepository, LeadsRepository>();
            container.RegisterType<ILeadsUnitTransactionRepository, LeadsUnitTransactionRepository>();
            container.RegisterType<IScenarioRepository, ScenarioRepository>();
            container.RegisterType<IMasterStatusRepository, MasterStatusRepository>();
            container.RegisterType<IDealerRepository, DealerRepository>();
            container.RegisterType<ISuspectFollowUpRepository, SuspectFollowUpRepository>();
            container.RegisterType<ICustomerProfileRefRepository, CustomerProfileRefRepository>();
            container.RegisterType<IMainDealerRepository, MainDealerRepository>();
            container.RegisterType<IScenarioSettingRepository, ScenarioSettingRepository>();
            container.RegisterType<IUnitPriceSettingRepository, UnitPriceSettingRepository>();
            container.RegisterType<IScenarioLeadMappingRepository, ScenarioLeadMappingRepository>();
            container.RegisterType<IScenarioFilterRepository, ScenarioFilterRepository>();
            container.RegisterType<IProvinceRepository, ProvinceRepository>();
            container.RegisterType<IKabupatenRepository, KabupatenRepository>();
            container.RegisterType<IKecamatanRepository, KecamatanRepository>();
            container.RegisterType<IKelurahanRepository, KelurahanRepository>();
            container.RegisterType<IProspectRepository, ProspectRepository>();
            container.RegisterType<IProspectFollowUpRepository, ProspectFollowUpRepository>();
            container.RegisterType<ISMSFollowupRepository, SMSFollowupRepository>();
            container.RegisterType<IEmployeeRepository, EmployeeRepository>();
            container.RegisterType<IScenarioHistoryRepository, ScenarioHistoryRepository>();
            container.RegisterType<IScriptRepository, ScriptRepository>();
            container.RegisterType<IScenarioScriptMappingRepository, ScenarioScriptMappingRepository>();
            container.RegisterType<IUnityTypeMarketRepository, UnityTypeMarketRepository>();
            container.RegisterType<IAnswerRepository, AnsweRepository>();

            // put all interface service and implementation here, if you using mvc controller
            container.RegisterType<IDummyService, DummyService>();
            container.RegisterType<ILeadsService, LeadsService>();
            container.RegisterType<IUploadLeadsService, UploadLeadsService>();
            container.RegisterType<IInactiveLeadsService, InactiveLeadsService>();
            container.RegisterType<IManageLeadsService, ManageLeadsService>();
            container.RegisterType<ISuspectService, SuspectService>();
            container.RegisterType<ISuspectFollowUpService, SuspectFollowUpService>();
            container.RegisterType<IScenarioSettingService, ScenarioSettingService>();
            container.RegisterType<IUnitPriceSettingService, UnitPriceSettingService>();
            container.RegisterType<IManageSendSmsService, ManageSendSmsService>();
            container.RegisterType<IScenarioService, ScenarioService>();
            container.RegisterType<IProspectToDealService, ProspectToDealService>();
            container.RegisterType<IFollowUpBySmsService, FollowUpBySmsService>();
            container.RegisterType<IScenarioApprovalDetailService, ScenarioApprovalDetailService>();
            container.RegisterType<IDistributeProspectService, DistributeProspectService>();
            container.RegisterType<IDistributeSuspectService, DistributeSuspectService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}