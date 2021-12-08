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
    public class PositionsController : ControllerBase
    {
        private readonly EmployeesContext db;

        public PositionsController(EmployeesContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Position>>> Get()
        {
            var positionList = await db.Positions.ToListAsync();
            return positionList;
        }

        [HttpGet("{position}")]
        public async Task<ActionResult<Position>> Get(string position)
        {
            var positionResult = await db.Positions.Where(p => p.PositionTitle.ToLower() == position.ToLower()).FirstOrDefaultAsync();
            return positionResult;
        }
    }
}