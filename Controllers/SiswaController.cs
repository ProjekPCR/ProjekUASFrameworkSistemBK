using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjekUASFrameworkSistemBK.Data;
using ProjekUASFrameworkSistemBK.Models;

namespace ProjekUASFrameworkSistemBK.Controllers
{
    public class SiswaController : Controller
    {
        private readonly ProjekUASFrameworkSistemBKContext _context;

        public SiswaController(ProjekUASFrameworkSistemBKContext context)
        {
            _context = context;
        }

        // GET: Siswa
        public async Task<IActionResult> Index()
        {
            var siswaList = await _context.Siswa.AsNoTracking().ToListAsync();
            return View(siswaList);

            //return View(await _context.Siswa.ToListAsync()); return View(await _context.Siswa.ToListAsync());
        }

        // GET: Siswa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var siswa = await _context.Siswa
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siswa == null) return NotFound();

            return View(siswa);
        }

        // GET: Siswa/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Siswa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nama,NIS,Kelas,JenisKelamin,Alamat,NoTelp")] SiswaViewModel siswa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(siswa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(siswa);
        }

        // GET: Siswa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var siswa = await _context.Siswa.FindAsync(id);
            if (siswa == null) return NotFound();

            return View(siswa);
        }

        // POST: Siswa/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nama,NIS,Kelas,JenisKelamin,Alamat,NoTelp")] SiswaViewModel siswa)
        {
            if (id != siswa.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(siswa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiswaExists(siswa.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(siswa);
        }

        // GET: Siswa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var siswa = await _context.Siswa
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siswa == null) return NotFound();

            return View(siswa);
        }

        // POST: Siswa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var siswa = await _context.Siswa.FindAsync(id);
            if (siswa != null)
            {
                _context.Siswa.Remove(siswa);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool SiswaExists(int id)
        {
            return _context.Siswa.Any(e => e.Id == id);
        }
    }
}
