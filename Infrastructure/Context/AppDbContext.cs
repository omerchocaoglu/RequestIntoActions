using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.EntitiyModels.ActionModels;
using Domain.EntitiyModels.AuditLogModels;
using Domain.EntitiyModels.RequestModels;
using Domain.EntitiyModels.UserModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) 
        {
        }
        public DbSet<Request> Requests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RequestAction> Actions { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
