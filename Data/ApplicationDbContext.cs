using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//good
namespace PRPicks.Models
{
   public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
  {
     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
     // Change to be your model(s) and table(s)
     public DbSet<Collection> Collections { get; set; }
     public DbSet<Product> Products { get; set; }
  }
}