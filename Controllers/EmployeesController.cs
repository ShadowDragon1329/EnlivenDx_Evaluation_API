using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EnlivenDX_Evaluation.Models;

namespace EnlivenDX_Evaluation.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EnlivenDxAssessmentsContext _context;

        public EmployeesController(EnlivenDxAssessmentsContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var enlivenDxAssessmentsContext = _context.Employees.Include(e => e.EmpDesignationNavigation).Include(e => e.EmpMgr);
            return View(await enlivenDxAssessmentsContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.EmpDesignationNavigation)
                .Include(e => e.EmpMgr)
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["EmpDesignation"] = new SelectList(_context.Designations, "DesignationId", "DesignationId");
            ViewData["EmpMgrId"] = new SelectList(_context.Employees, "EmpId", "EmpId");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpId,EmpFirstName,EmpLastName,EmpTechSkills,EmpDateOfJoining,EmpDesignation,EmpMgrId,EmpSalary")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpDesignation"] = new SelectList(_context.Designations, "DesignationId", "DesignationId", employee.EmpDesignation);
            ViewData["EmpMgrId"] = new SelectList(_context.Employees, "EmpId", "EmpId", employee.EmpMgrId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["EmpDesignation"] = new SelectList(_context.Designations, "DesignationId", "DesignationId", employee.EmpDesignation);
            ViewData["EmpMgrId"] = new SelectList(_context.Employees, "EmpId", "EmpId", employee.EmpMgrId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpId,EmpFirstName,EmpLastName,EmpTechSkills,EmpDateOfJoining,EmpDesignation,EmpMgrId,EmpSalary")] Employee employee)
        {
            if (id != employee.EmpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmpId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpDesignation"] = new SelectList(_context.Designations, "DesignationId", "DesignationId", employee.EmpDesignation);
            ViewData["EmpMgrId"] = new SelectList(_context.Employees, "EmpId", "EmpId", employee.EmpMgrId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.EmpDesignationNavigation)
                .Include(e => e.EmpMgr)
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmpId == id);
        }
    }
}
