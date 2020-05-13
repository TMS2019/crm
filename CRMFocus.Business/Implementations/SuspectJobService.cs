using AutoMapper;
using CRMFocus.DataAccess;
using CRMFocus.Entity;
using System;
using System.Linq;

namespace CRMFocus.Business.Implementations
{
    public class SuspectJobService
    {
        public static void MoveToInactiveIfUntil5DaysNoResponse(int count)
        {
            using (var db = new CRMFocusContext())
            {
                var suspects = db.Suspects.ToList();
                var suspectFollowUps = db.SuspectFollowUp.Where(w => w.IsDeleted == false).ToList();

                // Based on create date if suspect data after created is no response in 5 days then move to inactive
                var suspect5DaysNoResponseCreate = suspects.Where(w => DateTime.Now.AddDays(-count) >
                w.CreatedTime && w.LastReactivate == null && w.IsInactive == false && w.IsDeleted == false).ToList();
                foreach (var item in suspect5DaysNoResponseCreate)
                {
                    var isExsist = suspectFollowUps.Where(w => w.SuspectID == item.SuspectID && w.IsDeleted == false).FirstOrDefault();
                    if (isExsist == null)
                    {
                        var entity = db.Suspects.Where(w => w.SuspectID == item.SuspectID).FirstOrDefault();
                        entity.IsInactive = true;
                        db.SaveChanges();
                    }
                }

                // Based on reactivated date if suspect data after reactivated no response in 5 days then move to inactive
                var suspect5DaysNoResponseReactivated = suspects.Where(w => DateTime.Now.AddDays(-count) > w.LastReactivate && w.LastReactivate != null && w.IsInactive == false && w.IsDeleted == false).ToList();
                foreach (var item in suspect5DaysNoResponseReactivated)
                {
                    var isExsist = suspectFollowUps.Where(w => w.SuspectID == item.SuspectID && w.IsDeleted == false).FirstOrDefault();
                    if (isExsist == null)
                    {
                        var entity = db.Suspects.Where(w => w.SuspectID == item.SuspectID).FirstOrDefault();
                        entity.IsInactive = true;
                        db.SaveChanges();
                    }
                }
            }
        }

        public static void SoftDeleteSuspectWorkloadTypeIfAfter7DaysNoResponse(int count)
        {
            using (var db = new CRMFocusContext())
            {
                var suspects = db.Suspects.ToList();
                var suspectFollowUps = db.SuspectFollowUp.Where(w => w.IsDeleted == false).ToList();

                // Based on create date if suspect data after created is no response in 7 days then soft delete
                var suspectWorkloadType7DaysNoResponseCreate = suspects.Where(w => DateTime.Now.AddDays(-count) > w.CreatedTime && w.LastReactivate == null && w.IsDeleted == false).ToList();
                foreach (var item in suspectWorkloadType7DaysNoResponseCreate)
                {
                    var isExsist = suspectFollowUps.Where(w => w.SuspectID == item.SuspectID).Take(3).OrderBy(o => o.CreatedTime);
                    if (isExsist.LastOrDefault() != null && isExsist.LastOrDefault().CallStatus == 4) // 4 is WorkLoad
                    {
                        var suspect = db.Suspects.Where(w => w.SuspectID == item.SuspectID).FirstOrDefault();
                        suspect.IsDeleted = true;
                        db.SaveChanges();

                        foreach (var i in isExsist)
                        {
                            var suspectFollowUp = db.SuspectFollowUp.Where(w => w.SuspectFollowupID == i.SuspectFollowupID && w.IsDeleted == false).FirstOrDefault();
                            suspectFollowUp.IsDeleted = true;
                            db.SaveChanges();
                        }
                    }
                }


                // Based on reactivated date if suspect data after reactivated no response  in 7 days then soft delete
                var suspectWorkloadType7DaysNoResponseAfterReactivated = suspects.Where(w => DateTime.Now.AddDays(-count) > w.LastReactivate && w.LastReactivate != null && w.IsDeleted == false).ToList();
                foreach (var item in suspectWorkloadType7DaysNoResponseAfterReactivated)
                {
                    var isExsist = suspectFollowUps.Where(w => w.SuspectID == item.SuspectID).Take(3).OrderBy(o => o.CreatedTime);
                    if (isExsist.LastOrDefault() != null && isExsist.LastOrDefault().CallStatus == 4) // 4 is WorkLoad
                    {
                        var suspect = db.Suspects.Where(w => w.SuspectID == item.SuspectID).FirstOrDefault();
                        suspect.IsDeleted = true;
                        db.SaveChanges();

                        foreach (var i in isExsist)
                        {
                            var suspectFollowUp = db.SuspectFollowUp.Where(w => w.SuspectFollowupID == i.SuspectFollowupID && w.IsDeleted == false).FirstOrDefault();
                            suspectFollowUp.IsDeleted = true;
                            db.SaveChanges();
                        }
                    }
                }
            }
        }

        public static void MoveSuspectToProspectBasedOnNextFollowUpDate()
        {
            using (var db = new CRMFocusContext())
            {
                var suspects = db.Suspects.Where(w => w.IsDeleted == true).ToList(); // apakah yang dijadiin prospek cuman yg contacted aja ?
                var suspectFollowUps = db.SuspectFollowUp.Where(w => w.IsDeleted == false).ToList();

                foreach (var item in suspects)
                {
                    var suspectFollowUp = suspectFollowUps.Where(w => w.SuspectID == item.SuspectID && w.CallStatus == 5).FirstOrDefault();
                    if (suspectFollowUp.NextFollowupDate.Date == DateTime.Now.Date)
                    {
                        var prospect = new Prospect();

                        Mapper.Map(item, prospect);
                        prospect.CreatedTime = DateTime.Now;
                        prospect.CreatedBy = "System";
                        prospect.LastModifiedTime = null;
                        prospect.LastModifiedBy = null;
                        prospect.IsDeleted = false;

                        db.Prospects.Add(prospect);
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
