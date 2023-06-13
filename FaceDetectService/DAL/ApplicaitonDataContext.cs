using FaceDetectService.Models;
using Microsoft.EntityFrameworkCore;

namespace FaceDetectService.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<detectoperation>().ToTable("detectoperation");
            // Rest of your code
        }

        public DbSet<detectoperation> Operations { get; set; }
    }
}
