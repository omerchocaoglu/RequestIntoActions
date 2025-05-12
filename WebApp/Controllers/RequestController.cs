using Application.Repositories;
using Domain.DTOModels;
using Domain.EntitiyModels.RequestModels;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class RequestController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RequestController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> Index( int page = 1, int pageSize = 5 )
        {
            var userIdStr = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userIdStr))
            {
                return RedirectToAction("Login", "Account");
            }
            int userID = int.Parse(userIdStr);
            var requests = await _unitOfWork.Requests.GetAllAsync(r => r.UserID == userID);
            var totalCount = requests.Count();
            // pagination işlemleri için
            var pagedRequests = requests.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var viewModel = new RequestListViewModel
            {
                Requests = pagedRequests,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };

            return View(viewModel);
        }
        // Bu List sadece Ajax için tablo döner
        [HttpGet]
        public async Task<IActionResult> List(int page = 1, int pageSize = 5)
        {
            var userIdStr = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userIdStr))
            {
                return Unauthorized();
            }
            int userID = int.Parse(userIdStr);
            var requests = await _unitOfWork.Requests.GetAllAsync( r => r.UserID == userID);
            var totalCount = requests.Count();
            var pagedRequests = requests.Skip((page -1 ) * pageSize ).Take(pageSize).ToList();
            var viewModel = new RequestListViewModel
            {
                Requests = pagedRequests,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
            return PartialView("_RequestListPartial", viewModel);
        }
        // Create
        [HttpGet]
        public IActionResult Create()
        {
            var userIdStr = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userIdStr))
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RequestCreateDTO dto)
        {
            try
            {
                var userIdStr = HttpContext.Session.GetString("UserID");
                if (string.IsNullOrEmpty(userIdStr))
                {
                    return Unauthorized();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var request = new Request
                {
                    Title = dto.Title,
                    Description = dto.Description,
                    StartedDate = DateTime.Now,
                    UserID = int.Parse(userIdStr)
                };

                await _unitOfWork.Requests.AddAsync(request);
                await _unitOfWork.CompleteAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                // Geçici loglama
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }
        }

        // Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int ID)
        {
            // ajax ile yapılmış hali
            var userIdStr = HttpContext.Session.GetString("UserID");
            if ( string.IsNullOrEmpty(userIdStr))
            {
                return Unauthorized();
            }
            var request = await _unitOfWork.Requests.GetByIdAsync(ID);
            if ( request == null || request.UserID != int.Parse(userIdStr))
            {
                return NotFound();
            }
            var dto = new RequestUpdateDTO
            {
                ID = request.ID,
                Title = request.Title,
                Description = request.Description
            };
            return PartialView("_RequestEditPartial", dto);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RequestUpdateDTO dto)
        {
            var userIdStr = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userIdStr))
                return Unauthorized();

            var request = await _unitOfWork.Requests.GetByIdAsync(dto.ID);
            if (request == null || request.UserID != int.Parse(userIdStr))
                return NotFound();

            request.Title = dto.Title;
            request.Description = dto.Description;
            request.Message = dto.Message;

            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        //Delete
        [HttpPost] // ajax ile yapılan delete metodu
        public async Task<IActionResult> Delete(int ID)
        {
            var userIdStr = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userIdStr))
            {
                return Unauthorized();
            }
            var request = await _unitOfWork.Requests.GetByIdAsync(ID);
            if (request == null || request.UserID != int.Parse(userIdStr))
            {
                return NotFound();
            }
            _unitOfWork.Requests.Delete(request);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }
    }
}
