using Exercise2Data.Configuration;
using Exercise2Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Exercise2Data.EF
{
    public class EmployeeDBContext : IdentityDbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PositionConfiguration());
            builder.ApplyConfiguration(new EmployeeConfiguration());
            builder.Seed();
        }
        public EmployeeDBContext(DbContextOptions options) : base(options)
        {
        }
        DbSet<Position> Positions { get; set; }
        DbSet<Employee> Employees { get; set; }
    }
}
