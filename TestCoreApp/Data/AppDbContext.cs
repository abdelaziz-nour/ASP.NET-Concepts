using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestCoreApp.Areas.Employees.Models;
using TestCoreApp.Models;

namespace TestCoreApp.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData
                (
                    new Category() { Id = 1, Name = "Select" },
                    new Category() { Id = 2, Name = "CPU" },
                    new Category() { Id = 3, Name = "RAM" },
                    new Category() { Id = 4, Name = "HDD" },
                    new Category() { Id = 5, Name = "SSD" }
                );

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                },
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User",
                }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}