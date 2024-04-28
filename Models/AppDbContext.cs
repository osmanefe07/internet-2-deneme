using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using vize.Models;
namespace vize.Models
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
    public AppDbContext(DbContextOptions options) : base(options) 
        {

        }
        public DbSet<Dosya> Dosyas { get; set; }

        public DbSet<Klasor> Klasors { get; set;}

        public DbSet<UserGroup> UserGroups { get; set; }

       
    }


}
