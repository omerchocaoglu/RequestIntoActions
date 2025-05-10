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
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserID")))
            {
                return RedirectToAction("Index", "Home");
            }
            // Yetkili kullanıcıysa devam et
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userRepository.GetUserAsync(username, password);
            if (user == null)
            {
                ViewBag.Error = "Kullanıcı bulunamadı.";
                return View();
            }
            if (user != null) 
            {
                HttpContext.Session.SetString("UserID", user.ID.ToString());
                HttpContext.Session.SetString("Username", user.Email);
                HttpContext.Session.SetString("UserRole", user.FullName);

                return RedirectToAction("Index", "Request");
            }
            ViewBag.Error = "Geçersiz kullanacı adı veya şifre";
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // tüm sessionları temizle
            return RedirectToAction("Login", "Account");
        }
    }
}
