using System.Linq;
using System.Threading.Tasks;
using Indeavor.Test.Abstractions.Dtos;
using Indeavor.Test.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace Indeavor.Test.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : IndeavorBaseController
    {
        private readonly IRetrieveEmployeeService _retrieveEmployeeService;
        private readonly IEditEmployeeService _editEmployeeService;

        public EmployeesController(IRetrieveEmployeeService retrieveEmployeeService, IEditEmployeeService editEmployeeService)
        {
            _retrieveEmployeeService = retrieveEmployeeService;
            _editEmployeeService = editEmployeeService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return await HandleInvalidModelAsync();
            }
            var employeeResult = await _retrieveEmployeeService.GetByIdWithDepartmentAsync(id);
            if (employeeResult.Employee == null)
            {
                return NotFound();
            }
            if (!employeeResult.Succeeded)
            {
                return BadRequest(employeeResult.Errors.Select(x => x));
            }

            return Ok(employeeResult);
        }

        [HttpGet("{eNumber}", Name = "GetByENumber")]
        public async Task<IActionResult> Get(string eNumber)
        {
            if (!ModelState.IsValid)
            {
                return await HandleInvalidModelAsync();
            }
            var employeeResult = await _retrieveEmployeeService.GetByENumberWithDepartmentAsync(eNumber);
            if (employeeResult.Employee == null)
            {
                return NotFound();
            }
            if (!employeeResult.Succeeded)
            {
                return BadRequest(employeeResult.Errors.Select(x => x));
            }

            return Ok(employeeResult);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return await HandleInvalidModelAsync();
            }
            var employeesResult = await _retrieveEmployeeService.GetAllAsync();
            if (employeesResult.Employees == null || !employeesResult.Employees.Any())
            {
                return NotFound();
            }
            if (!employeesResult.Succeeded)
            {
                return BadRequest(employeesResult.Errors.Select(x => x));
            }

            return Ok(employeesResult);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeDto employee)
        {
            if (!ModelState.IsValid)
            {
                return await HandleInvalidModelAsync();
            }
            var employeeResult = await _retrieveEmployeeService.QuerySingleAsync(x => x.Email == employee.Email || x.ENumber == employee.ENumber);
            if (employeeResult.Employee != null)
            {
                return BadRequest($"An employee with {employee.Email} email or {employee.ENumber} number already exists.");
            }
            var succeeded = await _editEmployeeService.AddOrUpdateAsync(employee);
            if (succeeded)
            {
                employeeResult = await _retrieveEmployeeService.QuerySingleAsync(x => x.ENumber == employee.ENumber);

                return CreatedAtRoute("GetByENumber", new { eNumber = employeeResult.Employee?.ENumber }, employeeResult);
            }

            return Conflict("The new employee failed to be created.");
        }

        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] EmployeeDto employee)
        {
            if (!ModelState.IsValid)
            {
                return await HandleInvalidModelAsync();
            }
            var succeeded = await _editEmployeeService.AddOrUpdateAsync(employee);
            if (succeeded)
            {
                var employeeResult = await _retrieveEmployeeService.QuerySingleAsync(x => x.ENumber == employee.ENumber);

                return CreatedAtRoute("GetByENumber", new { eNumber = employeeResult.Employee?.ENumber }, employeeResult);
            }

            return Conflict("Employee failed to be updated.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return await HandleInvalidModelAsync();
            }
            var employeeResult = await _retrieveEmployeeService.GetByIdAsync(id);
            var employeeExists = employeeResult.Employee != null;
            if (!employeeExists)
            {
                return NotFound();
            }
            var succeeded = await _editEmployeeService.RemoveAsync(id);
            if (succeeded)
            {
                return Ok();
            }

            return Conflict($"Employee with {id} ID failed to be deleted.");
        }
    }
}