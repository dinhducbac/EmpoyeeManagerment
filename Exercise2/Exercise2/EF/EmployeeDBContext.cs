using Exercise2.Configuration;
using Exercise2.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Exercise2.EF
{
    public class EmployeeDBContext : DbContext
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
        public DbSet<Position> Positions { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
