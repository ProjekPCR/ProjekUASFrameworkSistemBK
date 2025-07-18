using Microsoft.AspNetCore.Mvc;
using ProjekUASFrameworkSistemBK.Data;
using ProjekUASFrameworkSistemBK.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProjekUASFrameworkSistemBK.Controllers
{
    public class RegistrasiController : Controller
    {
        private readonly ProjekUASFrameworkSistemBKContext _context;

        public RegistrasiController(ProjekUASFrameworkSistemBKContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Registrasi()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrasi(RegistrasiViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Cek apakah email sudah digunakan
            if (_context.Registrasi.Any(u => u.Username == model.Username))
            {
                ModelState.AddModelError("Username", "Email sudah terdaftar.");
                return View(model);
            }

            var user = new Registrasi
            {
                FullName = model.FullName,
                Username = model.Username,
                Password = model.Password // ⚠️ Sebaiknya hash password!
            };

            _context.Registrasi.Add(user);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Registrasi berhasil!";
            return RedirectToAction("Login", "Login"); // pastikan UserController dan Login.cshtml ada
        }
    }
}