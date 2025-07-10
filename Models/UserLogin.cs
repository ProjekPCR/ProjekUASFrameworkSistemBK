using System.ComponentModel.DataAnnotations;

public class UserLogin
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; }

    [Required]
    [MaxLength(255)]
    public string Password { get; set; }
}
