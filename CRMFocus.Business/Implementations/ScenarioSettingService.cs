using System;
using CRMFocus.Business.Interfaces;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Domain;
using System.Collections.Generic;
using System.Linq;
using CRMFocus.Entity;
using CRMFocus.Common;
using System.Transactions;
using System.Data.Entity.Core;

namespace CRMFocus.Business.Implementations
{
    public class ScenarioSettingService : BaseService<ScenarioSetting>, IScenarioSettingService
    {
        private readonly IScenarioSettingRepository _scenarioSettingRepository;

        public ScenarioSettingService(IScenarioSettingRepository scenarioSettingRepository)
        : base(scenarioSettingRepository)
        {
            _scenarioSettingRepository = scenarioSettingRepository;
        }

        public List<ScenarioSettingView> GetAllGetAllCustomScenario()
        {
            var list = new List<ScenarioSettingView>();
            var scenarioSettings = _scenarioSettingRepository.GetAll().Where(w => w.IsDeleted == false && w.Scenario.DestinationType == 3 && w.Scenario.isDefault == 0);
            foreach (var item in scenarioSettings)
            {
                var scenarioSettingView = new ScenarioSettingView()
                {
                    ScenarioSettingViewId = item.ScenarioSettingCode,
                    ScenarioName = item.Scenario.ScenarioName,
                    isAutomatic = item.isAutomatic,
                    SMSContent = item.Sms.SmsContent,
                    SmsMax = item.MaxSms,
                    DataSortByDirection = item.DataSortByDirection,
                    StartDistributionSmsDate = item.StartDistributionSmsDate,
                    EndDistributionSmsDate = item.EndDistributionSmsDate,
                    isActive = item.isActive
                };

                list.Add(scenarioSettingView);
            }

            return list;
        }

        public List<ScenarioSettingView> GetAllScenarioSetting(int destination)
        {
            var list = new List<ScenarioSettingView>();
            var scenarioSettings = _scenarioSettingRepository.GetAll().Where(w => w.IsDeleted == false && w.Scenario.DestinationType == destination && w.Scenario.isDefault == 1);
            foreach (var item in scenarioSettings)
            {
                var scenarioSettingView = new ScenarioSettingView()
                {
                    ScenarioSettingViewId = item.ScenarioSettingCode,
                    ScenarioName = item.Scenario.ScenarioName,
                    isAutomatic = item.isAutomatic,
                    SMSContent = item.Sms.SmsContent,
                    SmsMax = item.MaxSms,
                    DataSortByDirection = item.DataSortByDirection,
                    StartDistributionSmsDate = item.StartDistributionSmsDate,
                    EndDistributionSmsDate = item.EndDistributionSmsDate,
                    isActive = item.isActive
                };

                list.Add(scenarioSettingView);
            }

            return list;
        }

        public ScenarioSettingView Update(string id, ScenarioSettingView newEntity)
        {

            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    var oldEntity = _scenarioSettingRepository.GetAll().Where(w => w.ScenarioSettingCode == id).FirstOrDefault();
                    oldEntity.DataSortByDirection = newEntity.DataSortByDirection;
                    oldEntity.Scenario.ScenarioName = newEntity.ScenarioName;
                    oldEntity.isAutomatic = newEntity.isAutomatic;
                    oldEntity.Sms.SmsContent = newEntity.SMSContent;
                    oldEntity.MaxSms = newEntity.SmsMax;
                    oldEntity.StartDistributionSmsDate = newEntity.StartDistributionSmsDate;
                    oldEntity.EndDistributionSmsDate = newEntity.EndDistributionSmsDate;
                    oldEntity.isActive = newEntity.isActive;
                    _scenarioSettingRepository.Update(oldEntity);

                    ts.Complete();
                }
            }
            catch (TransactionAbortedException ex)
            {
                throw ex;
            }
            catch (OptimisticConcurrencyException ex)
            {
                throw ex;
            }
            catch (UpdateException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return newEntity;
        }
    }
}
