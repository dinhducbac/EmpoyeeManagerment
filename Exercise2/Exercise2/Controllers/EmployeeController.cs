using Exercise2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeManagerment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public readonly IEmployeeService EmployeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            EmployeeService = employeeService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await EmployeeService.GetEmployee(id);
            if(result.ResultObject != null)
            {
                return Ok(result.ResultObject);
            }
            return BadRequest(result);
        }
    }
}
