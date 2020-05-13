using CRMFocus.Business.Implementations;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Web.Configuration;

namespace CRMFocus.Presentation.App_Start
{
    public class JobSchedulerConfig
    {
        public static void Start()
        {
            try
            {
                var scheduler = StdSchedulerFactory.GetDefaultScheduler();
                scheduler.Start();

                //-- Delete temporary every 3 month
                var deleteTemporaryLeadJob = JobBuilder.Create<LeadTemporaryJob>()
                      .WithIdentity("deleteTemporaryLeadJob", "deleteTemporaryLeadGroup")
                      .Build();

                var deleteTemporaryLeadTrigger = TriggerBuilder.Create()
                        .WithDailyTimeIntervalSchedule
                        (s =>
                            s.WithIntervalInHours(24)
                            .OnEveryDay()
                            .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                        )
                        .Build();
                

                //-- Move suspect to inactive job
                var suspectMoveToInactiveJob = JobBuilder.Create<SuspectJob>()
                      .WithIdentity("suspectMoveToInactiveJob", "suspectGroup")
                      .Build();

                var suspectMoveToInactiveTrigger = TriggerBuilder.Create()
                        .WithDailyTimeIntervalSchedule
                        (s =>
                            s.WithIntervalInHours(24)
                            .OnEveryDay()
                            .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                        )
                        .Build();

                
                //-- Soft delete suspect with workload type
                var suspectSoftDeleteWorkloadJob = JobBuilder.Create<SuspectJob>()
                     .WithIdentity("suspectSoftDeleteWorkloadJob", "suspectGroup")
                     .Build();

                var suspectSoftDeleteWorkloadTrigger = TriggerBuilder.Create()
                        .WithDailyTimeIntervalSchedule
                        (s =>
                             s.WithIntervalInHours(24)
                            .OnEveryDay()
                            .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                        )
                        .Build();


                //-- Move suspect to prospect
                var suspectMoveToProspectJob = JobBuilder.Create<SuspectJob>()
                    .WithIdentity("suspectMoveToProspectJob", "suspectGroup")
                    .Build();

                var suspectMoveToProspectTrigger = TriggerBuilder.Create()
                        .WithDailyTimeIntervalSchedule
                        (s =>
                            s.WithIntervalInHours(24)
                            .OnEveryDay()
                            .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                        )
                        .Build();



                //-- Upload lead to DWAI every 3hrs
                var countUploadLeadDWAI = Convert.ToInt32(WebConfigurationManager.AppSettings["UploadToDWAIcount"]);
                var uploadTemporaryLeadJob = JobBuilder.Create<UploadLeadTemporaryJob>()
                      .WithIdentity("uploadTemporaryLeadJob", "uploadTemporaryLeadGroup")
                      .Build();

                var uploadTemporaryLeadTrigger = TriggerBuilder.Create()
                        .WithDailyTimeIntervalSchedule
                        (s =>
                            s.WithIntervalInHours(countUploadLeadDWAI)
                            .OnEveryDay()
                            .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                        )
                        .Build();


                var SendSMSScenarioJob = JobBuilder.Create<ScenarioJob>()
                   .WithIdentity("SendSMSScenarioJob", "scenarioGroup")
                   .Build();

                var SendSMSScenarioTrigger = TriggerBuilder.Create()
                        .WithDailyTimeIntervalSchedule
                        (s =>
                             // s.WithIntervalInSeconds(60)
                             s.WithIntervalInHours(24)
                            .OnEveryDay()
                            .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                        )
                        .Build();

                //-- Set all scheduler
                scheduler.ScheduleJob(deleteTemporaryLeadJob, deleteTemporaryLeadTrigger);
                scheduler.ScheduleJob(suspectMoveToInactiveJob, suspectMoveToInactiveTrigger);
                scheduler.ScheduleJob(suspectSoftDeleteWorkloadJob, suspectSoftDeleteWorkloadTrigger);
                scheduler.ScheduleJob(suspectMoveToProspectJob, suspectMoveToProspectTrigger);
                scheduler.ScheduleJob(uploadTemporaryLeadJob, uploadTemporaryLeadTrigger);
                scheduler.ScheduleJob(SendSMSScenarioJob, SendSMSScenarioTrigger);
            }
            catch (ArgumentException ex)
            {
                throw ex; ;
            }

        }
    }

    public class LeadTemporaryJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            //-- Scheduling to delete temporary lead and lead unit transaction every 3 months
            var count = WebConfigurationManager.AppSettings["DeleteLeadAndUnitTemporaryTable"] == "" ? 3 : Convert.ToInt32(WebConfigurationManager.AppSettings["DeleteLeadAndUnitTemporaryTable"]);
            LeadJobService.Delete3MonthsDataInTemporaryTable(count);           
        }
    }

    public class UploadLeadTemporaryJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            //-- Scheduling upload leads 
            var baseAddress = WebConfigurationManager.AppSettings["BaseAddress"];
            var credentials = new Dictionary<string, string>
            {
                {"grant_type", WebConfigurationManager.AppSettings["GrantTypeDWAI"]},
                {"username", WebConfigurationManager.AppSettings["UserNameDWAI"]},
                {"password", WebConfigurationManager.AppSettings["PasswordDWAI"]},
            };

            LeadJobService.UploadTemporaryLeadEvery3HrToDWAI(baseAddress, credentials);
        }
    }

    public class SuspectJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            //-- Scheduling move suspect to inactive
            var moveSuspectToInactiveCount = WebConfigurationManager.AppSettings["MoveSuspectToInactiveCount"] == "" ? 5 : Convert.ToInt32(WebConfigurationManager.AppSettings["MoveSuspectToInactiveCount"]);
            SuspectJobService.MoveToInactiveIfUntil5DaysNoResponse(moveSuspectToInactiveCount);

            //-- Scheduling soft delete workload suspect
            var softDeleteWorkloadSuspectCount = WebConfigurationManager.AppSettings["SoftDeleteWorkloadSuspectCount"] == "" ? 7 : Convert.ToInt32(WebConfigurationManager.AppSettings["SoftDeleteWorkloadSuspectCount"]);
            SuspectJobService.SoftDeleteSuspectWorkloadTypeIfAfter7DaysNoResponse(softDeleteWorkloadSuspectCount);

            //-- Scheduling move suspect to prospect based on next follow up date
            SuspectJobService.MoveSuspectToProspectBasedOnNextFollowUpDate();
        }
    }

    public class ScenarioJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var SMSGatewayAPI = WebConfigurationManager.AppSettings["SMSGatewayAPI"];
            ScenarioDistributionJobService.SendSMSAsync(SMSGatewayAPI);
        }
    }

}