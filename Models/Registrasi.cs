using System.ComponentModel.DataAnnotations;

namespace ProjekUASFrameworkSistemBK.Models
{
    public class Registrasi
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama lengkap wajib diisi")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email wajib diisi")]
        [EmailAddress(ErrorMessage = "Format email tidak valid")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password wajib diisi")]
        public string Password { get; set; }

        // ⛔ Jangan tambahkan ConfirmPassword di sini
    }
}