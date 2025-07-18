using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjekUASFrameworkSistemBK.Models
{
    public class KonselingViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Nama { get; set; }

        [Required]
        public string NIS { get; set; }

        [Required]
        public DateTime Tanggal { get; set; }

        [Required]
        public string Topik { get; set; }

        [Required]
        public string Media { get; set; }

        [Required]
        public string Catatan { get; set; }
    }
}