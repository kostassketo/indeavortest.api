using System.Linq;
using System.Threading.Tasks;
using Indeavor.Test.Abstractions.Dtos;
using Indeavor.Test.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace Indeavor.Test.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : IndeavorBaseController
    {
        private readonly IRetrieveDepartmentService _retrieveDepartmentService;
        private readonly IEditDepartmentService _editDepartmentService;

        public DepartmentsController(IRetrieveDepartmentService retrieveDepartmentService, IEditDepartmentService editDepartmentService)
        {
            _retrieveDepartmentService = retrieveDepartmentService;
            _editDepartmentService = editDepartmentService;
        }

        [HttpGet("{id:int}", Name = "GetById")]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return await HandleInvalidModelAsync();
            }
            var departmentResult = await _retrieveDepartmentService.GetByIdAsync(id);
            if (departmentResult.Department == null)
            {
                return NotFound();
            }
            if (!departmentResult.Succeeded)
            {
                return BadRequest(departmentResult.Errors.Select(x => x));
            }

            return Ok(departmentResult);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return await HandleInvalidModelAsync();
            }
            var departmentsResult = await _retrieveDepartmentService.GetAllAsync();
            if (departmentsResult.Departments == null || !departmentsResult.Departments.Any())
            {
                return NotFound();
            }
            if (!departmentsResult.Succeeded)
            {
                return BadRequest(departmentsResult.Errors.Select(x => x));
            }

            return Ok(departmentsResult);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DepartmentDto department)
        {
            if (!ModelState.IsValid)
            {
                return await HandleInvalidModelAsync();
            }
            var departmentResult = await _retrieveDepartmentService.QuerySingleAsync(x => x.Code == department.Code);
            if (departmentResult.Succeeded)
            {
                return BadRequest($"A department with { department.Code } code already exists.");
            }
            var succeeded = await _editDepartmentService.AddOrUpdateAsync(department);
            if (succeeded)
            {
                departmentResult = await _retrieveDepartmentService.QuerySingleAsync(x => x.Code == department.Code);

                return CreatedAtRoute("GetById", new { id = departmentResult.Department?.Id }, departmentResult.Department);
            }

            return Conflict("The new department failed to be created.");
        }

        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] DepartmentDto department)
        {
            if (!ModelState.IsValid)
            {
                return await HandleInvalidModelAsync();
            }
            var succeeded = await _editDepartmentService.AddOrUpdateAsync(department);
            if (succeeded)
            {
                var departmentResult = await _retrieveDepartmentService.QuerySingleAsync(x => x.Code == department.Code);

                return CreatedAtRoute("GetById", new { id = departmentResult.Department?.Id }, departmentResult.Department);
            }

            return Conflict("Department failed to be updated.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return await HandleInvalidModelAsync();
            }
            if (_retrieveDepartmentService.GetByIdAsync(id) == null)
            {
                return NotFound();
            }
            var succeeded = await _editDepartmentService.RemoveAsync(id);
            if (succeeded)
            {
                return Ok();
            }

            return Conflict($"Department with {id} ID failed to be deleted.");
        }
    }
}