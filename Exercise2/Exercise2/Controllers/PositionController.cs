using EmployeeManagerment.Models;
using EmployeeManagerment.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeManagerment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        public readonly IPositionService PositionService;
        public PositionController(IPositionService positionService)
        {
            PositionService = positionService;
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await PositionService.GetAll();
            return Ok(result.ResultObject);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await PositionService.GetPositionById(id);
            if (result.ResultObject != null)
            {
                return Ok(result.ResultObject);
            }
            return BadRequest(result);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(PositionCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await PositionService.Create(request);
            if (result.ResultObject != null)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPut("{id}/update")]
        public async Task<IActionResult> Update(int id, PositionUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await PositionService.Update(id, request);
            if (result.ResultObject != null)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await PositionService.Delete(id);
            if (result.Success == true)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
