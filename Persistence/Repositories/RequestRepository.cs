using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories;
using Domain.EntitiyModels.RequestModels;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class RequestRepository: Repository<Request>, IRequestRepository
    {
        private readonly AppDbContext _context;
        public RequestRepository(AppDbContext context) : base(context) 
        { 
            _context = context;
        }
        // Sadece bu repository'e özel metotlar burada tanımlanabilir:
    }
}
