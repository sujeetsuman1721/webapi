using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RestaurantTruam.Models
{
    public class SecurityDbContext : IdentityDbContext
    {

        public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options)
        {
        }


    }
    
}
