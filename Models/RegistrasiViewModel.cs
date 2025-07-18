using System.ComponentModel.DataAnnotations;

namespace ProjekUASFrameworkSistemBK.Models
{
    public class RegistrasiViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
