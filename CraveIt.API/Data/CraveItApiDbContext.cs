using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CraveIt.API.Data
{
    // CraveItApiDbContext represents the database context for the app
    public class CraveItApiDbContext : IdentityDbContext<IdentityUser>
    {
        // Constructor of CraveItApiDbContext
        // Param: DbContextOptions<CraveItApiDbContext>,
        // which contains configuration options for the context
        public CraveItApiDbContext(DbContextOptions<CraveItApiDbContext> options)
            : base(options)
        {
        }

        // Method called when building the model for the context
        // It overrides the OnModelCreating method of the base class
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Models.Quiz> Quiz { get; set; }

        public DbSet<Models.Result> Result { get; set; }
    }
}
