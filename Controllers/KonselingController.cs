using Microsoft.AspNetCore.Mvc;
using ProjekUASFrameworkSistemBK.Models;
using ProjekUASFrameworkSistemBK.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Drawing;
using NuGet.Packaging;



namespace ProjekUASFrameworkSistemBK.Controllers
{
    public class KonselingController : Controller
    {
        private readonly ProjekUASFrameworkSistemBKContext _context;

        public KonselingController(ProjekUASFrameworkSistemBKContext context)
        {
            _context = context;
        }

        // LIST / INDEX
        public async Task<IActionResult> Index()
        {
            var data = await _context.Konseling
                .Select(k => new KonselingViewModel
                {
                    Id = k.Id,
                    Nama = k.Nama,
                    NIS = k.NIS,
                    Tanggal = k.Tanggal,
                    Topik = k.Topik,
                    Media = k.Media,
                    Catatan = k.Catatan
                })
                .ToListAsync();

            return View("Index", data); // ini akan menampilkan Index.cshtml
        }

        // UNDUL LAPORAN
        public IActionResult UnduhLaporan()
        {
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))

            {
                var worksheet = package.Workbook.Worksheets.Add("Riwayat Konseling");

                worksheet.Cells[1, 1].Value = "No";
                worksheet.Cells[1, 2].Value = "Nama";
                worksheet.Cells[1, 3].Value = "NIS";
                worksheet.Cells[1, 4].Value = "Tanggal";
                worksheet.Cells[1, 5].Value = "Topik";
                worksheet.Cells[1, 6].Value = "Media";
                worksheet.Cells[1, 7].Value = "Catatan";

                var data = _context.Konseling.ToList();

                int row = 2;
                int no = 1;
                foreach (var item in data)
                {
                    worksheet.Cells[row, 1].Value = no++;
                    worksheet.Cells[row, 2].Value = item.Nama;
                    worksheet.Cells[row, 3].Value = item.NIS;
                    worksheet.Cells[row, 4].Value = item.Tanggal.ToString("yyyy-MM-dd");
                    worksheet.Cells[row, 5].Value = item.Topik;
                    worksheet.Cells[row, 6].Value = item.Media;
                    worksheet.Cells[row, 7].Value = item.Catatan;
                    row++;
                }

                package.Save();
            }

            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Riwayat_Konseling.xlsx");
        }

        // CREATE GET
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KonselingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine("MODEL ERROR: " + modelError.ErrorMessage);
                }

                TempData["Error"] = "Validasi gagal. Cek semua isian.";
                return View(model);
            }

            var entity = new Konseling
            {
                Nama = model.Nama,
                NIS = model.NIS,
                Tanggal = model.Tanggal,
                Topik = model.Topik,
                Media = model.Media,
                Catatan = model.Catatan
            };

            _context.Konseling.Add(entity);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Catatan konseling berhasil ditambahkan.";
            return RedirectToAction(nameof(Index));
        }

        // EDIT GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var entity = await _context.Konseling.FindAsync(id);
            if (entity == null) return NotFound();

            var viewModel = new KonselingViewModel
            {
                Id = entity.Id,
                Nama = entity.Nama,
                NIS = entity.NIS,
                Tanggal = entity.Tanggal,
                Topik = entity.Topik,
                Media = entity.Media,
                Catatan = entity.Catatan
            };

            return View(viewModel);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KonselingViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var entity = await _context.Konseling.FindAsync(id);
                if (entity == null) return NotFound();

                entity.Nama = model.Nama;
                entity.NIS = model.NIS;
                entity.Tanggal = model.Tanggal;
                entity.Topik = model.Topik;
                entity.Media = model.Media;
                entity.Catatan = model.Catatan;

                await _context.SaveChangesAsync();
                TempData["Success"] = "Catatan konseling berhasil diperbarui.";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // DELETE GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var entity = await _context.Konseling.FindAsync(id);
            if (entity == null) return NotFound();

            var viewModel = new KonselingViewModel
            {
                Id = entity.Id,
                Nama = entity.Nama,
                NIS = entity.NIS,
                Tanggal = entity.Tanggal,
                Topik = entity.Topik,
                Media = entity.Media,
                Catatan = entity.Catatan
            };

            return View(viewModel);
        }

        // DELETE POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entity = await _context.Konseling.FindAsync(id);
            if (entity != null)
            {
                _context.Konseling.Remove(entity);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Catatan berhasil dihapus.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
