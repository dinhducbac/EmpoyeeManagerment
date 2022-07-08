using Exercise2.Entity;
using Exercise2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exercise2.Services
{
    public interface IEmployeeService
    {
        public Task<APIResult<EmployeeViewModel>> GetEmployee(int id);
        public Task<APIResult<EmployeeViewModel>> Create(EmployeeCreateRequest request);
    }
}
