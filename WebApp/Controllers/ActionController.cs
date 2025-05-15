using Application.Repositories;
using Domain.DTOModels;
using Domain.EntitiyModels.ActionModels;
using Domain.EntitiyModels.UserModels;
using Domain.Enums;
using Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;
using System.Linq;

namespace WebApp.Controllers
{
    public class ActionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public ActionController(AppDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Add(int RequestID)
        {
            try
            {
                var dto = new RequestActionCreateDto
                {
                    RequestID = RequestID,
                };
                return PartialView("_AddActionPartial", dto);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add(RequestActionCreateDto dto)
        {
            try
            {
                var userIdStr = HttpContext.Session.GetString("UserID");
                if (ModelState.IsValid)
                {
                    var entity = new RequestAction
                    {

                        UserID = int.Parse(userIdStr),
                        RequestID = dto.RequestID,
                        Message = dto.Message,
                        Description = dto.Description,
                        StartedDate = dto.StartedDate,
                        FinishedDate = dto.FinishedDate,
                        CreatedBy = int.Parse(userIdStr),
                        ObjectStatus = ObjectStatus.NonDeleted,
                        Status = Status.Active
                        
                    };
                    _context.Actions.Add(entity);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return PartialView("_AddActionPartial", dto);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IActionResult> ActionsByRequest (int RequestID)
        {
            try
            {
                var actions =await _context.Actions
                .Where(a => a.RequestID == RequestID && a.ObjectStatus == ObjectStatus.NonDeleted)
                .Include(a => a.User)
                .OrderByDescending(a => a.StartedDate)
                .Select(a => new RequestActionListDto
                {
                ID = a.ID,
                Message = a.Message,
                Description = a.Description,
                CreatedAt = a.StartedDate,
                UserName = a.User.Name,
                Email = a.User.Email
                })
                .ToListAsync();

                return Json(actions);
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Edit Action
        [HttpGet]
        public async Task<IActionResult> EditAction(int ID)
        {
            try
            {
                var action = await _unitOfWork.RequestActions.GetByIdAsync(ID);
                if (action == null || action.ObjectStatus == ObjectStatus.Deleted)
                {
                    return NotFound();
                }
                var dto = new RequestActionUpdateDTO
                {
                    ID = action.ID,
                    RequestID = action.RequestID,
                    Message = action.Message,
                    Description = action.Description,
                    StartedDate = action.StartedDate,
                    FinishedDate = action.FinishedDate
                };
                return PartialView("_EditActionPartial", dto);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditAction(RequestActionUpdateDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var userIdStr = HttpContext.Session.GetString("UserID");
                if (string.IsNullOrEmpty(userIdStr))
                {
                    return Unauthorized();
                }

                var existing = await _unitOfWork.RequestActions.GetByIdAsync(dto.ID);
                if ( existing == null || existing.ObjectStatus == ObjectStatus.Deleted )
                {
                    return NotFound();
                }
                existing.Message = dto.Message;
                existing.Description = dto.Description;
                existing.LastModifiedBy = int.Parse(userIdStr);
                existing.LastModifiedOn = DateTime.Now;

                _unitOfWork.RequestActions.Update(existing);
                await _unitOfWork.CompleteAsync();
                return Ok();    
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Delete Action
        [HttpPost]
        public async Task<IActionResult> DeleteAction(int ID)
        {
            try
            {
                var userIdStr = HttpContext.Session.GetString("UserID");
                var action = await _unitOfWork.RequestActions.GetByIdAsync(ID);
                if (action == null)
                {
                    return NotFound();
                }
                action.ObjectStatus = ObjectStatus.Deleted;
                action.Status = Status.Passive;
                action.StartedDate = DateTime.Now;
                action.FinishedDate = DateTime.Now;
                action.LastModifiedBy = int.Parse(userIdStr);
                action.LastModifiedOn = DateTime.Now;

                _unitOfWork.RequestActions.Update(action);
                await _unitOfWork.CompleteAsync();

                return Ok();
            }
            catch (Exception exception)
            {

                return BadRequest(exception.Message);
            }
        }
    }
}
