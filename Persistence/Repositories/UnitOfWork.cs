using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IRequestRepository Requests { get; }

        public IRequestActionRepository RequestActions { get; }

        public UnitOfWork(AppDbContext context, IRequestRepository requestRepository)
        {
            _context = context;
            Requests = requestRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
