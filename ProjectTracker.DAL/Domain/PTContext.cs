using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectTracker.Models;
using System.Data.Entity;

namespace ProjectTracker.Domain
{
    public class PTContext : DbContext
    {
        public PTContext() : base("DefaultConnection")
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().HasMany(c => c.Employees)
                .WithMany(s => s.Projects)
                .Map(t => t.MapLeftKey("ProjectId")
                .MapRightKey("EmployeeId")
                .ToTable("ProjectEmployee"));
        }
    }
}