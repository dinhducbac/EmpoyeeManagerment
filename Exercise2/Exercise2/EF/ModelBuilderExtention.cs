using Exercise2.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Exercise2.EF
{
    public static class ModelBuilderExtention 
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Position>().HasData(
                new Position
                {
                    Id = 1,
                    Name = "Mentor"
                },
                new Position
                {
                    Id= 2,
                    Name = "OM"
                }
            );
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "Nguyen Quoc Au",
                    PositionID = 1
                },
                new Employee
                {
                    Id = 2,
                    Name = "Nguyen Thanh Hai",
                    PositionID = 2
                }
            );
        }
    }
}
