using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories;
using Domain.EntitiyModels.ActionModels;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class RequestActionRepository: Repository<RequestAction>, IRequestActionRepository
    {
        public RequestActionRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<RequestAction>> GetByRequestIdAsync(int requestID)
        {
            return await _context.Actions.Where(a => a.RequestID == requestID).OrderByDescending(a => a.StartedDate).ToListAsync();
        }
    }
}
