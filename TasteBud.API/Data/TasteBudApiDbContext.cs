using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TasteBud.API.Data
{
    // TasteBudApiDbContext represents the database context for the app
    public class TasteBudApiDbContext : IdentityDbContext<IdentityUser>
    {
        // Constructor of TasteBudApiDbContext
        // Param: DbContextOptions<TasteBudApiDbContext>,
        // which contains configuration options for the context
        public TasteBudApiDbContext(DbContextOptions<TasteBudApiDbContext> options)
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
