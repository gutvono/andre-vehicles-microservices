using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndreVehicles.Data;
using Models;
using AndreVehicles.EmployeeApi.Services;

namespace AndreVehicles.EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AndreVehiclesContext _context;
        private readonly EmployeeService _employeeService;

        public EmployeesController(AndreVehiclesContext context, EmployeeService employeeService)
        {
            _context = context;
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            return await _context.Employee.Include(e => e.Address).ToListAsync();
        }

        [HttpGet("{document}")]
        public async Task<ActionResult<Employee>> GetEmployee(string document)
        {
            var employee = await _context.Employee.Where(e => e.Document == document).Include(e => e.Address).FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        //[HttpPut("{document}")]
        //public async Task<IActionResult> PutEmployee(string document, Employee employee)
        //{
        //    if (document != employee.Document)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(employee).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EmployeeExists(document))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(EmployeeDTO employeeDTO)
        {
            var employee = new Employee(employeeDTO);

            try
            {
                employee.Address = await _employeeService.GetAddress(employee.Address);
                _context.Entry(employee.Address).State = EntityState.Modified;
                _context.Employee.Add(employee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmployeeExists(employee.Document))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEmployee", new { document = employee.Document }, employee);
        }

        //[HttpDelete("{document}")]
        //public async Task<IActionResult> DeleteEmployee(string document)
        //{
        //    var employee = await _context.Employee.FindAsync(document);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Employee.Remove(employee);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool EmployeeExists(string document)
        {
            return _context.Employee.Any(e => e.Document == document);
        }
    }
}
