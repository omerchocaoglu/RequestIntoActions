using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IUnitOfWork:IDisposable
    {
        IRequestRepository Requests { get; }
        IRequestActionRepository RequestActions { get; }
        Task<int> CompleteAsync(); // async SaveChanges
    }
}
