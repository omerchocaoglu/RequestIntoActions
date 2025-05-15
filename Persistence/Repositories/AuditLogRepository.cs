using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories;
using Domain.EntitiyModels.AuditLogModels;
using Infrastructure.Context;

namespace Persistence.Repositories
{
    public class AuditLogRepository : Repository<AuditLog>, IAuditLogRepository
    {
        private readonly AppDbContext _context;
        public AuditLogRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
