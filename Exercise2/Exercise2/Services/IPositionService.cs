using EmployeeManagerment.Models;
using Exercise2.Entity;
using Exercise2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagerment.Services
{
    public interface IPositionService
    {
        public Task<APIResult<List<Position>>> GetAll();
        public Task<APIResult<Position>> Create(PositionCreateRequest request);
        public Task<APIResult<Position>> GetPositionById(int id);
        public Task<APIResult<Position>> Update(int id, PositionUpdateRequest request);
        public Task<APIResult<string>> Delete(int id);
    }
}
