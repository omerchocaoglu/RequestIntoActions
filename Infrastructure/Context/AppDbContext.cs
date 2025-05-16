//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Domain.EntitiyModels.ActionModels;
//using Domain.EntitiyModels.AuditLogModels;
//using Domain.EntitiyModels.RequestModels;
//using Domain.EntitiyModels.UserModels;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Options;

//namespace Infrastructure.Context
//{
//    public class AppDbContext: DbContext
//    {
//        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) 
//        {
//        }
//        public DbSet<Request> Requests { get; set; }
//        public DbSet<User> Users { get; set; }
//        public DbSet<RequestAction> Actions { get; set; }
//        public DbSet<AuditLog> AuditLogs { get; set; }
//        public override int SaveChanges()
//        {
//            return base.SaveChanges();
//        }
//    }
//}




using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.EntitiyModels.ActionModels;
using Domain.EntitiyModels.AuditLogModels;
using Domain.EntitiyModels.RequestModels;
using Domain.EntitiyModels.UserModels;
using Infrastructure.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Request> Requests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RequestAction> Actions { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        // Audit log alanı aşağıdadır.
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Audit için değişiklikleri algıla
            var auditEntries = OnBeforeSaveChanges();

            // Önce normal veri kayıtlarını yap
            var result = await base.SaveChangesAsync(cancellationToken);

            // Sonra AuditLog kayıtlarını ekle ama ChangeTracker'ı tetiklemeden
            if (auditEntries.Any())
            {
                foreach (var auditEntry in auditEntries)
                {
                    var log = auditEntry.ToAuditLog();
                    AuditLogs.Attach(log); // Takip etmeden ekle
                    Entry(log).State = EntityState.Added;
                }

                // Audit loglarını kaydet, hata olursa yakala
                try
                {
                    await base.SaveChangesAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    // Hatalı durumda loglama yapılabilir
                    Console.WriteLine("Audit log kaydederken hata oluştu: " + ex.Message);
                }
            }

            return result;
        }

        // Bu kısım AuditLog için eklendi
        private List<AuditEntry> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();

            foreach (var entry in ChangeTracker.Entries())
            {
                // AuditLog kaydının kendisini loglamaya çalışmıyoruz
                if (entry.Entity is AuditLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                string operationType = entry.State switch
                {
                    EntityState.Added => "CREATE",
                    EntityState.Modified => "UPDATE",
                    EntityState.Deleted => "DELETE",
                    _ => entry.State.ToString()
                };

                var auditEntry = new AuditEntry(entry)
                {
                    TableName = entry.Metadata.GetTableName(),
                    OperationType = operationType,
                };

                foreach (var prop in entry.Properties)
                {
                    string propName = prop.Metadata.Name;

                    if (prop.Metadata.IsPrimaryKey())
                        continue;

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.NewValues[propName] = prop.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.OldValues[propName] = prop.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (prop.IsModified)
                            {
                                auditEntry.OldValues[propName] = prop.OriginalValue;
                                auditEntry.NewValues[propName] = prop.CurrentValue;
                            }
                            break;
                    }
                }

                auditEntry.Changes = string.Join(", ", auditEntry.NewValues.Keys);
                auditEntries.Add(auditEntry);
            }

            return auditEntries;
        }
    }
}

