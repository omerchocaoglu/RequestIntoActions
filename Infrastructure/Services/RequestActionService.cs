using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories;
using Application.Services;
using Domain.DTOModels;
using Domain.EntitiyModels.ActionModels;


namespace Infrastructure.Services
{
    public class RequestActionService: IRequestActionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RequestActionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(RequestActionCreateDto dto)
        {
            var action = new RequestAction
            {
                RequestID = dto.RequestID,
                Description = dto.Description,
                StartedDate = DateTime.Now
            };
            await _unitOfWork.RequestActions.AddAsync(action);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<RequestActionDto>> GetByRequestIdAsync(int requestID)
        {
            var actions = await _unitOfWork.RequestActions.GetByRequestIdAsync(requestID);
            return actions.Select(a => new RequestActionDto 
            {
                ID = a.ID,
                RequestID = a.RequestID,
                Description = a.Description,
                ActionDate = a.StartedDate
            }).ToList();
        }
    }
}
