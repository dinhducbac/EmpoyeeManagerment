using Exercise2Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Exercise2Data.EF
{
    public static class ModelBuilderExtention 
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Position>().HasData(
                new Position
                {
                    Id = new System.Guid("FFA30612-3D31-47E2-B2BC-EC180E065C06"),
                    Name = "Mentor"
                },
                new Position
                {
                    Id= new System.Guid("DA233E5B-2FDB-4DAA-A817-3932647B8B5F"),
                    Name = "OM"
                }
            );
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = new System.Guid("D5828B9F-12DD-4B9E-9AB4-59A215507D22"),
                    Name = "Nguyen Quoc Au",
                    PositionID = Guid.Parse("FFA30612-3D31-47E2-B2BC-EC180E065C06")
                },
                new Employee
                {
                    Id = new System.Guid("FAA9195B-B4BB-4CFC-8806-6D3DD0528152"),
                    Name = "Nguyen Thanh Hai",
                    PositionID = Guid.Parse("DA233E5B-2FDB-4DAA-A817-3932647B8B5F")
                }
            );
        }
    }
}
