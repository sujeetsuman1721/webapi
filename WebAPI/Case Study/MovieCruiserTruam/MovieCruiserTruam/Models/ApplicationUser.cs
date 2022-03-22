using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MovieCruiserTruam.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Email { get; set; }   
        public string PhoneNumber { get; set; }
      

        public string Password { get; set; }
        [Compare("Password", ErrorMessage ="PasswordResetMessage>>.ErrorMessage")]
        public string ConfirmPassword { get; set; } 
        
    }
}
