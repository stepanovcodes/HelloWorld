using Microsoft.EntityFrameworkCore;
using HelloWorld.Models;
namespace HelloWorld.Data
{
    public class DataContextEF : DbContext
    {
        public DbSet<Computer>? Computer { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("Server=localhost;Database=DotNetCourseDatabase;Trusted_Connection=false;TrustServerCertificate=True;User Id=sa;Password=SQLConnect1;",
                options => options.EnableRetryOnFailure());
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TutorialAppSchema");

            modelBuilder.Entity<Computer>()
            // .HasNoKey();
            .HasKey(c => c.ComputerId);
            // .ToTable("Computer", "TutorialAppSchema");
            // .ToTable("TableName", "SchemaName");
        }
    }
}