using System.ComponentModel.DataAnnotations;

namespace ProjekUASFrameworkSistemBK.Models
{
    public class SiswaViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nama { get; set; }

        [Required]
        public string NIS { get; set; }

        public string Kelas { get; set; }

        public string JenisKelamin { get; set; }

        public string Alamat { get; set; }

        public string NoTelp { get; set; }
    }
}