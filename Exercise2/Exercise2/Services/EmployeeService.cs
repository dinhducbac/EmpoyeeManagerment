using Exercise2.EF;
using Exercise2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Exercise2.Entity;

namespace Exercise2.Services
{
    public class EmployeeService : IEmployeeService
    {
        public readonly EmployeeDBContext Db;
        public EmployeeService(EmployeeDBContext db)
        {
            Db = db;
        }

        public Task<APIResult<EmployeeViewModel>> Create(EmployeeCreateRequest request)
        {
            throw new System.NotImplementedException();
        }

        public async Task<APIResult<EmployeeViewModel>> GetEmployee(int id)
        {
            var employees = await Db.Employees.Where(emp=>emp.Id == id)
                .Join(
                    Db.Positions, 
                    emp => emp.PositionID,
                    pos => pos.Id,
                    (emp,pos) => new { emp.Id,emp.Name, Position = pos.Name}
                ).FirstOrDefaultAsync();
            var employeeViewModel = new EmployeeViewModel() { Id = employees.Id, Name = employees.Name, Position = employees.Position };
            return new APIResult<EmployeeViewModel>() { Success = true, Message = "Success", ResultObject = employeeViewModel };
        }
    }
}
