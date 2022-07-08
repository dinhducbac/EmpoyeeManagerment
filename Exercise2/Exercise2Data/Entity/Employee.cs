using System;

namespace Exercise2Data.Entity
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid PositionID { get; set; }
        public Position Position { get; set; }
    }
}
