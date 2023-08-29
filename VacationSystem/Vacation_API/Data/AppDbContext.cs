using Vacation_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Vacation_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<LocalUser> Users { get; set; }
        public DbSet<HistVacation> HistVacations { get; set; }
        public DbSet<MyDetails> myDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                Id = 1,
                Name = "Admin",
                Salary = 10100.00,
                entryDate = DateTime.Parse("17/10/2022"),
                Email = "admin@gmail.com",
                Occupancy = "PM"
            });



            modelBuilder.Entity<LocalUser>().HasData(new LocalUser
            {
                Id = 1,
                Name = "Admin",
                userName = "admin@gmail.com",
                Password = "123", //Default password
                Role = "PM",
                Email = "admin@gmail.com",

            });
        }
    }
}
