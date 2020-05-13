using AutoMapper;
using CRMFocus.Domain;
using CRMFocus.Entity;

namespace CRMFocus.Presentation.App_Start
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            Mapper.CreateMap<Dummy, Dummy>();
            Mapper.CreateMap<CreateLeadsView, LeadsTemporary>();
            Mapper.CreateMap<UploadLeadsView, LeadsTemporary>();
            Mapper.CreateMap<UploadLeadsView, LeadsUnitTransactionTemporary>();
            Mapper.CreateMap<Suspect, Suspect>();
            Mapper.CreateMap<UploadLeadsView, Lead>();
            Mapper.CreateMap<UploadLeadsView, LeadsUnitTransaction>();
            Mapper.CreateMap<ScenarioSetting, ScenarioSetting>();
            Mapper.CreateMap<UnitPriceSetting, UnitPriceSetting>();
            Mapper.CreateMap<TambahDetailScenarioView, Scenario>();
            Mapper.CreateMap<TambahCustomerTargetingView, ScenarioFilter>();
            Mapper.CreateMap<Suspect, Prospect>();
            Mapper.CreateMap<LeadsUnitTransactionView, LeadsUnitTransactionTemporary>();
            Mapper.CreateMap<LeadsUnitTransactionView, LeadsUnitTransaction>();
            Mapper.CreateMap<CreateLeadsView, Lead>();
            Mapper.CreateMap<TambahDetailScenarioView, Scenario>();
        }
    }
}