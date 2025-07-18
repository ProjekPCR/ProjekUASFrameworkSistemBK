// File: Models/AccountViewModel/LoginViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace ProjekUASFrameworkSistemBK.Models
{
    public class LoginViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }
    }
}
