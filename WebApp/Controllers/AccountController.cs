using Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userRepository.GetUserAsync(username, password);
            if (user == null) 
            {
                HttpContext.Session.SetString("UserID", user.ID.ToString());
                HttpContext.Session.SetString("Username", user.Email);
                HttpContext.Session.SetString("UserRole", user.FullName);

                return RedirectToAction("Index", "Home");
            }
            ViewBag.Error = "Geçersiz kullanacı adı veya şifre";
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
