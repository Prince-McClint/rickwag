using System.ComponentModel.DataAnnotations;

namespace WordJumble.Shared;

public class UserProfile
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string CurrentPassword { get; set; }

    [Required]
    [StringLength(16, ErrorMessage = "sorry, password length must exceed 7 and be less than 16", MinimumLength = 8)]
    public string Password { get; set; }

    [Required]
    [Compare(nameof(Password), ErrorMessage = "sorry, passwords don't match")]
    public string ConfirmPassword { get; set; }

    public string PreviousUsername { get; set; }
}