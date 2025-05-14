using Domain.DTOModels;
using Domain.EntitiyModels.ActionModels;
using Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WebApp.Controllers
{
    public class ActionController : Controller
    {
        private readonly AppDbContext _context;
        public ActionController(AppDbContext context)
        {
            _context = context;
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
        public IActionResult ActionsByRequest (int RequestID)
        {
            try
            {
                var actions = _context.Actions
                .Where(a => a.RequestID == RequestID)
                .Include(a => a.User)
                .OrderByDescending(a => a.StartedDate)
                .Select(a => new RequestActionListDto
                {
                Description = a.Description,
                CreatedAt = a.StartedDate,
                UserName = a.User.Name,
                Email = a.User.Email
                })
                .ToList();

                return Json(actions);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
