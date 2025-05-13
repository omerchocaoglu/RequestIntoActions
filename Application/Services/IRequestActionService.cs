using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOModels;

namespace Application.Services
{
    public interface IRequestActionService
    {
        Task AddAsync(RequestActionCreateDto dto);
        Task<List<RequestActionDto>> GetByRequestIdAsync(int requestID);
    }
}
