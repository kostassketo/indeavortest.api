using Indeavor.Test.Model;
using Microsoft.EntityFrameworkCore;

namespace Indeavor.Test.DAL
{
    public class IndeavorTestDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }

        public IndeavorTestDbContext(DbContextOptions<IndeavorTestDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Employee>()
                .HasIndex(p => new { p.ENumber })
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(p => new { p.Email })
                .IsUnique();

            modelBuilder.Entity<Department>()
                .HasIndex(p => new { p.Code })
                .IsUnique();

            modelBuilder.Entity<Department>()
                .HasIndex(p => new { p.Name })
                .IsUnique();
        }
    }
}
