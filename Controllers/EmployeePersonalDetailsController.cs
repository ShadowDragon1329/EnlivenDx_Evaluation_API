using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EnlivenDX_Evaluation.Models;

namespace EnlivenDX_Evaluation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeePersonalDetailsController : ControllerBase
    {
        private readonly EnlivenDxAssessmentsContext _context;

        public EmployeePersonalDetailsController(EnlivenDxAssessmentsContext context)
        {
            _context = context;
        }

        // GET: api/EmployeePersonalDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeePersonalDetail>>> GetEmployeePersonalDetails()
        {
            return await _context.EmployeePersonalDetails.ToListAsync();
        }

        // GET: api/EmployeePersonalDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeePersonalDetail>> GetEmployeePersonalDetail(int id)
        {
            var employeePersonalDetail = await _context.EmployeePersonalDetails.FindAsync(id);

            if (employeePersonalDetail == null)
            {
                return NotFound();
            }

            return employeePersonalDetail;
        }

        // PUT: api/EmployeePersonalDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeePersonalDetail(int id, EmployeePersonalDetail employeePersonalDetail)
        {
            if (id != employeePersonalDetail.EmpId)
            {
                return BadRequest();
            }

            _context.Entry(employeePersonalDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeePersonalDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EmployeePersonalDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeePersonalDetail>> PostEmployeePersonalDetail(EmployeePersonalDetail employeePersonalDetail)
        {
            _context.EmployeePersonalDetails.Add(employeePersonalDetail);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmployeePersonalDetailExists(employeePersonalDetail.EmpId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEmployeePersonalDetail", new { id = employeePersonalDetail.EmpId }, employeePersonalDetail);
        }

        // DELETE: api/EmployeePersonalDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeePersonalDetail(int id)
        {
            var employeePersonalDetail = await _context.EmployeePersonalDetails.FindAsync(id);
            if (employeePersonalDetail == null)
            {
                return NotFound();
            }

            _context.EmployeePersonalDetails.Remove(employeePersonalDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeePersonalDetailExists(int id)
        {
            return _context.EmployeePersonalDetails.Any(e => e.EmpId == id);
        }
    }
}
