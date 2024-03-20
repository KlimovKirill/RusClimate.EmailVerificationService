using Microsoft.EntityFrameworkCore;
using RusClimate.EmailVerificationService.DAL.Postgres.EF.Models;

namespace RusClimate.EmailVerificationService.DAL.Postgres.EF
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Email> Emails { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
    }
}
