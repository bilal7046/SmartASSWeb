using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SmartASSWeb.Core;
using SmartASSWeb.Dal.Entities;

namespace SmartASS.DAL
{
    public class SmartAssDb
         : DbContext
    {
        public SmartAssDb() : base("name=SmartAssDb")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        
        #region IDbSets

        public DbSet<Activity> Activities { get; set; }
        public DbSet<AffiliateProfile> AffiliateProfiles { get; set; }
        public DbSet<AppSettings> AppSettings { get; set; }
        public DbSet<BusinessCard> BusinessCards { get; set; }
        public DbSet<CalendarEvent> CalendarEvents { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<CampaignAffiliate> CampaignAffiliates { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<CompanyType> CompanyTypes { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactGroup> ContactGroups { get; set; }
        public DbSet<ConversionGoal> ConversionGoals { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<LeadLogAction> LeadLogActions { get; set; }
        public DbSet<LeadNote> LeadNotes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TeamMemberGoal> TeamMemberGoals { get; set; }
        public DbSet<UserAction> UserActions { get; set; }

        #endregion

        #region OnSaveChanges

        public override int SaveChanges()
        {
            try
            {
                SetAudit();

                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException("Entity Validation Failed - errors follow:\n" + sb, ex); // Add the original exception as the innerException
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public override async Task<int> SaveChangesAsync()
        {
            try
            {
                SetAudit();

                return await base.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException("Entity Validation Failed - errors follow:\n" + sb, ex); // Add the original exception as the innerException
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                throw ex;
            }
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                SetAudit();

                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException("Entity Validation Failed - errors follow:\n" + sb, ex); // Add the original exception as the innerException
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Set Audit

        private void SetAudit()
        {
            var addedAuditedEntities = ChangeTracker.Entries<EntityBase>().Where(p => p.State == EntityState.Added)
                .Select(p => p.Entity);
            var modifiedAuditedEntities = ChangeTracker.Entries<EntityBase>().Where(p => p.State == EntityState.Modified)
                .Select(p => p.Entity);
            var now = DateTime.UtcNow;
            foreach (var added in addedAuditedEntities)
            {
                added.DateCreated = now;
                added.CreatedBy = OwinContextHelper.CurrentApplicationUser;
                added.DateUpdated = now;
                added.UpdatedBy = OwinContextHelper.CurrentApplicationUser;
            }

            foreach (var modified in modifiedAuditedEntities)
            {
                if (modified.DateCreated == DateTime.MinValue)
                    modified.DateCreated = now;
                if (string.IsNullOrEmpty(modified.CreatedBy))
                    modified.CreatedBy = OwinContextHelper.CurrentApplicationUser;
                modified.DateUpdated = now;
                modified.UpdatedBy = OwinContextHelper.CurrentApplicationUser;
            }
        }

        #endregion

        #region Set Audit

        private void SetAudit(string userId)
        {
            var addedAuditedEntities = ChangeTracker.Entries<EntityBase>().Where(p => p.State == EntityState.Added)
                .Select(p => p.Entity);
            var modifiedAuditedEntities = ChangeTracker.Entries<EntityBase>().Where(p => p.State == EntityState.Modified)
                .Select(p => p.Entity);
            var now = DateTime.UtcNow;
            foreach (var added in addedAuditedEntities)
            {
                added.DateCreated = now;
                added.CreatedBy = userId;
                added.DateUpdated = now;
                added.UpdatedBy = userId;
            }

            foreach (var modified in modifiedAuditedEntities)
            {
                if (modified.DateCreated == DateTime.MinValue)
                    modified.DateCreated = now;
                if (string.IsNullOrEmpty(modified.CreatedBy))
                    modified.CreatedBy = userId;
                modified.DateUpdated = now;
                modified.UpdatedBy = userId;
            }
        }

        #endregion
    }
}
