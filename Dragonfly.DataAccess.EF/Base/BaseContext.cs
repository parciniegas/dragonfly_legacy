using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using Dragonfly.Core;
using Dragonfly.Core.Configuration;
using Dragonfly.DataAccess.Core;
using Dragonfly.DataAccess.EF.Model;

namespace Dragonfly.DataAccess.EF.Base
{
    public abstract class BaseContext : DbContext
    {
        #region Private Fields
        private readonly IApplicationEnvironment _applicationEnvironment;
        private readonly ITracker _tracker;
        #endregion

        #region Constructors
        protected BaseContext(string connectionString)
            : base(connectionString)
        { }

        protected BaseContext(IApplicationEnvironment applicationEnvironment)
            : base(applicationEnvironment.GetConnectionString())
        {
            _applicationEnvironment = applicationEnvironment;
        }


        protected BaseContext(IApplicationEnvironment applicationEnvironment, ITracker tracker)
            : base(applicationEnvironment.GetConnectionString())
        {
            _applicationEnvironment = applicationEnvironment;
            _tracker = tracker;
        }
        #endregion

        #region DbSets
        public DbSet<Track> Tracks { get; set; }
        #endregion

        #region Overrided Methods
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}
        #endregion

        #region Public Methods
        public override int SaveChanges()
        {
            var now = DateTime.Now;
            AuditEntities(now);
            TrackEntities(now);

            return base.SaveChanges();
        }
        #endregion

        #region Private Methods
        private void AuditEntities(DateTime now)
        {
            var addedAuditedEntities = ChangeTracker.Entries<IAuditable>()
                .Where(p => p.State == EntityState.Added)
                .Select(p => p.Entity);
            foreach (var created in addedAuditedEntities)
            {
                created.CreatedOn = now;
                created.CreatedBy = _applicationEnvironment.GetCurrentUser();
                created.UpdatedOn = now;
                created.UpdatedBy = _applicationEnvironment.GetCurrentUser();
            }

            var modifiedAuditedEntities = ChangeTracker.Entries<IAuditable>()
                .Where(p => p.State == EntityState.Modified)
                .Select(p => p.Entity);
            foreach (var updated in modifiedAuditedEntities)
            {
                Entry((object)updated).Property("CreatedOn").IsModified = false;
                updated.UpdatedOn = now;
                updated.UpdatedBy = _applicationEnvironment.GetCurrentUser();
            }
        }

        private void TrackEntities(DateTime now)
        {
            var excludes = new[] { "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy" };
            var sb = new StringBuilder();
            var separator = ",";

            var trackedEntities = ChangeTracker.Entries<ITrackeable>()
              .Where(e => e.State != EntityState.Unchanged);

            foreach (var trackedEntity in trackedEntities)
            {
                var tracked = new Track
                {
                    EntityName = trackedEntity.Entity.GetType().Name,
                    EntityKey = GetPrimaryKeyValue(trackedEntity),
                    Action = trackedEntity.State.ToString(),
                    ModifiedBy = _applicationEnvironment.GetCurrentUser(),
                    ModifiedDate = now,
                    DataLog = string.Empty
                };

                sb.Clear();
                sb.Append("\"Changes\":[");

                switch (trackedEntity.State)
                {
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Added:
                        foreach (var prop in trackedEntity.CurrentValues.PropertyNames)
                        {
                            if (excludes.Contains(prop))
                                continue;

                            var currentValue = trackedEntity.CurrentValues[prop] == null
                                ? "Null"
                                : trackedEntity.CurrentValues[prop].ToString();

                            sb.Append("{{\"PropertyName\":\"{0}\", \"OriginalValue\":\"{1}\", \"CurrentValue\":\"{2}\"}}{3}".Inject(prop, "N/A", currentValue, separator));
                            separator = string.Empty;
                        }
                        break;
                    case EntityState.Modified:
                        foreach (var prop in trackedEntity.CurrentValues.PropertyNames)
                        {
                            if (excludes.Contains(prop))
                                continue;

                            var originalValue = (trackedEntity.OriginalValues[prop] != null
                                ? trackedEntity.OriginalValues[prop].ToString()
                                : "Null");
                            var currentValue = trackedEntity.CurrentValues[prop] == null
                                ? "Null"
                                : trackedEntity.CurrentValues[prop].ToString();

                            sb.Append("{{\"PropertyName\":\"{0}\", \"OriginalValue\":\"{1}\", \"CurrentValue\":\"{2}\"}}{3}".Inject(prop, originalValue, currentValue, separator));
                            separator = string.Empty;
                        }
                        break;
                    case EntityState.Deleted:
                        foreach (var prop in trackedEntity.OriginalValues.PropertyNames)
                        {
                            if (excludes.Contains(prop))
                                continue;

                            var originalValue = (trackedEntity.OriginalValues[prop] != null ? trackedEntity.OriginalValues[prop].ToString() : "Null");
                            sb.Append("{{\"PropertyName\":\"{0}\", \"OriginalValue\":\"{1}\", \"CurrentValue\":\"{2}\"}}{3}".Inject(prop, originalValue, "N/A", separator));
                            separator = string.Empty;
                        }
                        break;
                }

                sb.Append("]");

                tracked.DataLog = sb.ToString();
                if (_tracker == null)
                {
                    Tracks.Add(tracked);
                }
                else
                {
                    _tracker.TrackIt(tracked);
                }
            }
        }

        private string GetPrimaryKeyValue(DbEntityEntry entry)
        {
            var objectStateEntry =
                ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            return objectStateEntry.EntityKey.EntityKeyValues.IsNull() ? "Null" : objectStateEntry.EntityKey.EntityKeyValues[0].Value.ToString();
        }
        #endregion
    }
}
