#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheWall.Models;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [Display(Name = "First Name")]
    [MinLength(2,ErrorMessage = "First name must be at least 2 characters!")]
    public string? FirstName { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "Last name must be at least 2 characters!")]
    [Display(Name = "Last Name")]
    public string? LastName { get; set; }

    [Required]
    [EmailAddress]
    [UniqueEmail]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format")]
    public string? Email { get; set; }

    [Required]
    [MinLength(8,ErrorMessage = "Password must be at least 8 characters!")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Required]
    [Display(Name = "Confirm Password")]
    [NotMapped]
    [Compare("Password", ErrorMessage = "Password does not match!")]
    public string? ConfirmPassword { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public List<Comment> CommentsFromUsers { get; set; } = new List<Comment>();

    public List<Message> MessageFromUsers { get; set;} = new List<Message>();



    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Email is required!");
            }
            MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));
            if (_context.Users.Any(e => e.Email == value.ToString()))
            {
                return new ValidationResult("Email already exist!");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }


}