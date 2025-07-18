using Microsoft.AspNetCore.Mvc;
using ProjekUASFrameworkSistemBK.Data;
using ProjekUASFrameworkSistemBK.Models;
using System.Linq;

namespace ProjekUASFrameworkSistemBK.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ProjekUASFrameworkSistemBKContext _context;

        public DashboardController(ProjekUASFrameworkSistemBKContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Ambil jumlah siswa dari tabel Siswa
            var jumlahSiswa = _context.Siswa.Count();

            // Ambil semua data konseling
            var semuaKonseling = _context.Konseling.ToList();

            // Jumlah riwayat konseling = total yang sudah dilakukan (bisa pakai status jika ada)
            var jumlahRiwayat = semuaKonseling.Count(); // ubah jika ada status

            // Ambil data riwayat konseling terbaru (misal 5 data terakhir)
            var riwayatKonseling = semuaKonseling
                .OrderByDescending(k => k.Tanggal)
                .Select(k => new KonselingViewModel
                {
                    Id = k.Id,
                    Nama = k.Nama,
                    NIS = k.NIS,
                    Tanggal = k.Tanggal,
                    Topik = k.Topik,
                    Media = k.Media,
                    Catatan = k.Catatan
                }).Take(5).ToList();

            // Kirim data ke View
            ViewBag.JumlahSiswa = jumlahSiswa;
            ViewBag.JumlahRiwayat = jumlahRiwayat;
            ViewBag.RiwayatKonseling = riwayatKonseling;

            return View();
        }
    }
}