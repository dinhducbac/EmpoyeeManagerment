using System;
using System.Collections.Generic;

namespace Exercise2.Entity
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
