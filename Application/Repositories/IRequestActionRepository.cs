using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.EntitiyModels.ActionModels;

namespace Application.Repositories
{
    public interface IRequestActionRepository : IRepository<RequestAction>
    {
        Task<IEnumerable<RequestAction>> GetByRequestIdAsync(int  requestID);
    }
}
