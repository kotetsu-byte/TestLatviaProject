using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestLatviaProject.Interface;
using TestLatviaProject.Models;

namespace TestLatviaProject.Data
{
    public class TestDBContext : IdentityDbContext<User>
    {  
        public TestDBContext(DbContextOptions<TestDBContext> options) : base(options) { }
        
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<User> Users {  get; set; }
        public DbSet<UserAdmin> UserAdmins { get; set; }
        public DbSet<Audit> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {            
            base.OnModelCreating(builder);
        }

        public virtual async Task<int> SaveChangesAsync(string? userName = null)
        {
            OnBeforeSaveChanges(userName);
            var result = await base.SaveChangesAsync();
            return result;
        }

        public void OnBeforeSaveChanges(string? userName)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach(var entry in ChangeTracker.Entries())
            {
                if(entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                {
                    continue;
                }
                var auditEntry = new AuditEntry(entry)
                {
                    tableName = entry.Entity.GetType().Name,
                    userName = userName
                };
                auditEntries.Add(auditEntry);
                foreach(var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if(property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.keyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch(entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = Enums.AuditType.Create;
                            auditEntry.newValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.AuditType = Enums.AuditType.Delete;
                            auditEntry.oldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.changedColumns.Add(propertyName);
                                auditEntry.AuditType = Enums.AuditType.Update;
                                auditEntry.oldValues[propertyName] = property.OriginalValue;
                                auditEntry.newValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }
            foreach(var auditEntry in auditEntries)
            {
                AuditLogs.Add(auditEntry.ToAudit());
            }
        }
    }
}
