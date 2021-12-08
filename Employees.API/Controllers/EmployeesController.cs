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
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeesContext db;

        public EmployeesController(EmployeesContext context)
        {
            db = context;
            if (!db.Employees.Any())
            {
                db.Departments.Add(new Department { DepartmentTitle = "IT" });
                db.Departments.Add(new Department { DepartmentTitle = "Розница" });
                db.Departments.Add(new Department { DepartmentTitle = "Опт" });
                db.SaveChanges();

                db.Positions.Add(new Position { PositionTitle = "Системный администратор" });
                db.Positions.Add(new Position { PositionTitle = "Консультант" });
                db.Positions.Add(new Position { PositionTitle = "Менеджер" });
                db.SaveChanges();

                db.Employees.Add(new Employee
                {
                    LastName = "Иванов",
                    FirstName = "Иван",
                    Patronymic = "Иванович",
                    Phone = "12345678",
                    DepartmentId = 1,
                    PositionId = 1,
                    Salary = 12345
                });
                db.Employees.Add(new Employee
                {
                    LastName = "Сергеев",
                    FirstName = "Сергей",
                    Patronymic = "Сергеевич",
                    Phone = "5646464",
                    DepartmentId = 2,
                    PositionId = 2,
                    Salary = 5312
                });
                db.Employees.Add(new Employee
                {
                    LastName = "Александров",
                    FirstName = "Александр",
                    Patronymic = "Александрович",
                    Phone = "89746546",
                    DepartmentId = 3,
                    PositionId = 3,
                    Salary = 55485
                });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> Get()
        {
            var employeeList = await db.Employees.ToListAsync();
            return employeeList;
        }

        [HttpGet("{searchQuery}")]
        public async Task<ActionResult<IEnumerable<Employee>>> Get(string searchQuery)
        {
            var employeeList = await db.Employees.Where(
                e => e.LastName.ToLower().Contains(searchQuery.ToLower()) ||
                e.FirstName.ToLower().Contains(searchQuery.ToLower()) ||
                e.Patronymic.ToLower().Contains(searchQuery.ToLower()) ||
                e.Phone.ToLower().Contains(searchQuery.ToLower())
                ).ToListAsync();
            return employeeList;
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> Post(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }

            db.Employees.Add(employee);
            await db.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpPut]
        public async Task<ActionResult<Employee>> Put(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }
            if (!db.Employees.Any(x => x.Id == employee.Id))
            {
                return NotFound();
            }

            db.Update(employee);
            await db.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> Delete(int id)
        {
            Employee employee = db.Employees.FirstOrDefault(x => x.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            db.Employees.Remove(employee);
            await db.SaveChangesAsync();
            return Ok(employee);
        }
    }
}