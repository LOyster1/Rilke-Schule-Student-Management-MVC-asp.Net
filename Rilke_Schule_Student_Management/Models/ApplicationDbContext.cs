using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Rilke_Schule_Student_Management.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Guardianship> Guardianships { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Class> Classes { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}