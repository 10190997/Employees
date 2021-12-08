using Employees.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly EmployeesContext db;

        public DepartmentsController(EmployeesContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> Get()
        {
            var departmentList = await db.Departments.ToListAsync();
            return departmentList;
        }

        [HttpGet("{department}")]
        public async Task<ActionResult<Department>> Get(string department)
        {
            var departmentResult = await db.Departments.Where(d => d.DepartmentTitle.ToLower() == department.ToLower()).FirstOrDefaultAsync();
            return departmentResult;
        }
    }
}