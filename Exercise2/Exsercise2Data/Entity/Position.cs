using System;
using System.Collections.Generic;

namespace Exercise2Data.Entity
{
    public class Position
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
