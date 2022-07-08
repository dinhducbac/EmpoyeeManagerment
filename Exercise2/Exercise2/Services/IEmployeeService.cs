using EmployeeManagerment.Models;
using Exercise2.Entity;
using Exercise2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exercise2.Services
{
    public interface IEmployeeService
    {
        public Task<APIResult<List<EmployeeViewModel>>> GetAll();
        public Task<APIResult<EmployeeViewModel>> GetEmployee(int id);
        public Task<APIResult<EmployeeViewModel>> Create(EmployeeCreateRequest request);
        public Task<APIResult<EmployeeViewModel>> Update(int id, EmployeeUpdateRequest request);
        public Task<APIResult<string>> Delete(int id);
    }
}
