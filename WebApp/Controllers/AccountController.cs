﻿using Application.Repositories;
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
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserID")))
                {
                    return RedirectToAction("Index", "Home");
                }
                // Yetkili kullanıcıysa devam et
                return View();
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
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
                    HttpContext.Session.SetString("UserName", user.UserName);

                    return RedirectToAction("Index", "Request");
                }
                ViewBag.Error = "Geçersiz kullanacı adı veya şifre";
                return View();
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // ekstra güvenlik katmanı için gereklidir
        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Clear(); // tüm sessionları temizle oturumu sıfırla
                return RedirectToAction("Login", "Account");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
