using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories;
using Domain.EntitiyModels.RequestModels;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class RequestRepository: Repository<Request>, IRequestRepository
    {
        public RequestRepository(DbContext context) : base(context) 
        { 
        }
        // Sadece bu repository'e özel metotlar burada tanımlanabilir:
    }
}
