using Application.Repositories;
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
        public async Task<IActionResult> Create(Request request)
        {
            var userIdStr = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userIdStr))
            {
                return RedirectToAction("Login", "Account");
            }

            request.UserID = int.Parse(userIdStr);
            await _unitOfWork.Requests.AddAsync(request);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction("Index");
        }
        // Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int ID)
        {
            var userIdStr = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userIdStr))
            {
                return RedirectToAction("Login", "Account");
            }
            int userID = int.Parse(userIdStr);
            var request = await _unitOfWork.Requests.GetByIdAsync(ID);
            if (request == null || request.UserID != userID)
            {
                return Unauthorized();
            }
            return View(request);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Request request)
        {
            var userIdStr = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userIdStr))
            {
                return RedirectToAction("Login", "Account");
            }

            int userID = int.Parse(userIdStr);
            var exiting = await _unitOfWork.Requests.GetByIdAsync(request.ID);
            if (exiting == null || exiting.UserID != userID)
            {
                return Unauthorized();
            }

            //Güncellenecek kısım
            exiting.Title = request.Title;
            exiting.Description = request.Description;
            _unitOfWork.Requests.Update(exiting);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction("Index");
        }
        //Delete

        public async Task<IActionResult> Delete(int ID)
        {
            var userIdStr = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userIdStr)) 
            {
                return RedirectToAction("Login", "Account");
            }
            int userID = int.Parse(userIdStr);
            var request = await _unitOfWork.Requests.GetByIdAsync(ID);
            if (request == null || request.UserID != userID )
            {
                return Unauthorized();
            }
            
            _unitOfWork.Requests.Delete(request);
            await _unitOfWork.CompleteAsync();
            return RedirectToAction("Index");
        }
    }
}
