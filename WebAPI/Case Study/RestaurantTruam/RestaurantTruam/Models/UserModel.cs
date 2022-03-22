using System.ComponentModel.DataAnnotations;

namespace RestaurantTruam.Models
{
    public class UserModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string FullName { get; set; }
        public int? Age { get; set; }
    }
}
