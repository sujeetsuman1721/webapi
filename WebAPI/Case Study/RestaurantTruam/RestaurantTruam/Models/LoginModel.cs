using System.ComponentModel.DataAnnotations;

namespace RestaurantTruam.Controllers
{
    public class LoginModel
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}