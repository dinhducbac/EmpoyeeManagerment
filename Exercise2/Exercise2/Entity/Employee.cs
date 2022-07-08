using System;

namespace Exercise2.Entity
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PositionID { get; set; }
        public Position Position { get; set; }
    }
}
