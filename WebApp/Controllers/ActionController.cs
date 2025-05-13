using Domain.DTOModels;
using Domain.EntitiyModels.ActionModels;
using Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;

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
            var dto = new RequestActionCreateDto
            {
                RequestID = RequestID,
            };
            return PartialView("_AddActionPartial", dto);
        }
        [HttpPost]
        public async Task<IActionResult> Add(RequestActionCreateDto dto)
        {
            var userIdStr = HttpContext.Session.GetString("UserID");
            if(ModelState.IsValid)
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
    }
}
