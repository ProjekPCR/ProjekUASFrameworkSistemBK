using Microsoft.AspNetCore.Mvc;
using ProjekUASFrameworkSistemBK.Data;
using ProjekUASFrameworkSistemBK.Models;
using System.Linq;

namespace ProjekUASFrameworkSistemBK.Controllers
{
    public class LoginController : Controller
    {
        private readonly ProjekUASFrameworkSistemBKContext _context;

        public LoginController(ProjekUASFrameworkSistemBKContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Cek apakah ada user sesuai
            var user = _context.Registrasi
                .FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

            if (user != null)
            {
                // Simpan informasi login ke session
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("FullName", user.FullName);

                return RedirectToAction("Index", "Dashboard");
            }

            ViewBag.Error = "Username atau password salah.";
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}