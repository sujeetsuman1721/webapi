using Microsoft.AspNetCore.Identity;
using System;

namespace RestaurantTruam.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
        public int? Age { get; set; }

       
    }
}
