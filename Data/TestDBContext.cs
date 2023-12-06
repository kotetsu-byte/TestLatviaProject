using Microsoft.AspNetCore.Identity;
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

            // Roles
            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" });
            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole { Id = "2", Name = "Manager", NormalizedName = "MANAGER" });
            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole { Id = "3", Name = "User", NormalizedName = "USER" });

            var hasher = new PasswordHasher<User>();

            // Users
            builder.Entity<User>()
                .HasData(
                    new User
                    {
                        Id = "1",
                        UserName = "test_admin",
                        NormalizedUserName = "TEST_ADMIN",
                        PasswordHash = hasher.HashPassword(null, "admin")
                    }
                );

            builder.Entity<User>()
                .HasData(
                    new User
                    {
                        Id = "2",
                        UserName = "test_manager_1",
                        NormalizedUserName = "TEST_MANAGER_1",
                        PasswordHash = hasher.HashPassword(null, "manager_1")
                    }
                );

            builder.Entity<User>()
                .HasData(
                    new User
                    {
                        Id = "3",
                        UserName = "test_manager_2",
                        NormalizedUserName = "TEST_MANAGER_2",
                        PasswordHash = hasher.HashPassword(null, "manager_2")
                    }
                );

            builder.Entity<User>()
                .HasData(
                    new User
                    {
                        Id = "4",
                        UserName = "test_manager_3",
                        NormalizedUserName = "TEST_MANAGER_3",
                        PasswordHash = hasher.HashPassword(null, "manager_3")
                    }
                );

            builder.Entity<User>()
                .HasData(
                    new User
                    {
                        Id = "5",
                        UserName = "test_user_1",
                        NormalizedUserName = "TEST_USER_1",
                        PasswordHash = hasher.HashPassword(null, "user_1")
                    }
                );

            builder.Entity<User>()
                .HasData(
                    new User
                    {
                        Id = "6",
                        UserName = "test_user_2",
                        NormalizedUserName = "TEST_USER_2",
                        PasswordHash = hasher.HashPassword(null, "user_2")
                    }
                );

            builder.Entity<User>()
                .HasData(
                    new User
                    {
                        Id = "7",
                        UserName = "test_user_3",
                        NormalizedUserName = "TEST_USER_3",
                        PasswordHash = hasher.HashPassword(null, "user_3")
                    }
                );

            builder.Entity<User>()
                .HasData(
                    new User
                    {
                        Id = "8",
                        UserName = "test_user_4",
                        NormalizedUserName = "TEST_USER_4",
                        PasswordHash = hasher.HashPassword(null, "user_4")
                    }
                );

            builder.Entity<User>()
                .HasData(
                    new User
                    {
                        Id = "9",
                        UserName = "test_user_5",
                        NormalizedUserName = "TEST_USER_5",
                        PasswordHash = hasher.HashPassword(null, "user_5")
                    }
                );

            // Roles and Users Realtionship
            builder.Entity<IdentityUserRole<string>>()
                .HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "1",
                    UserId = "1"
                }
                );

            builder.Entity<IdentityUserRole<string>>()
                .HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "2",
                    UserId = "2"
                }
                );

            builder.Entity<IdentityUserRole<string>>()
                .HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "2",
                    UserId = "3"
                }
                );

            builder.Entity<IdentityUserRole<string>>()
                .HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "2",
                    UserId = "4"
                }
                );

            builder.Entity<IdentityUserRole<string>>()
                .HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "3",
                    UserId = "5"
                }
                );

            builder.Entity<IdentityUserRole<string>>()
                .HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "3",
                    UserId = "6"
                }
                );

            builder.Entity<IdentityUserRole<string>>()
                .HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "3",
                    UserId = "7"
                }
                );

            builder.Entity<IdentityUserRole<string>>()
                .HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "3",
                    UserId = "8"
                }
                );

            builder.Entity<IdentityUserRole<string>>()
                .HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "3",
                    UserId = "9"
                }
                );
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
