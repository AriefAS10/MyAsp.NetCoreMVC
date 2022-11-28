using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataPribadiNetCoreMVC.Data;
using DataPribadiNetCoreMVC.Models;

namespace DataPribadiNetCoreMVC.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly DataPribadiDbContext _context;

        public EmployeesController(DataPribadiDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var dataPribadiDbContext = _context.Employees.Include(e => e.DataDb).Include(e => e.JobTitle);
            return View(await dataPribadiDbContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.DataDb)
                .Include(e => e.JobTitle)
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
            ViewData["EmployeeName"] = new SelectList(_context.DataDbs, "Nik", "Nik");
            ViewData["JobTitles"] = new SelectList(_context.JobTitles, "Id", "JobTitle1");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpId,EmployeeName,JobTitles,Email,Phone,HiredDate,JobId,Country")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeName"] = new SelectList(_context.DataDbs, "Nik", "Nik", employee.EmployeeName);
            ViewData["JobTitles"] = new SelectList(_context.JobTitles, "Id", "JobTitle1", employee.JobTitles);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["EmployeeName"] = new SelectList(_context.DataDbs, "Nik", "Nik", employee.EmployeeName);
            ViewData["JobTitles"] = new SelectList(_context.JobTitles, "Id", "JobTitle1", employee.JobTitles);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EmpId,EmployeeName,JobTitles,Email,Phone,HiredDate,JobId,Country")] Employee employee)
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
            ViewData["EmployeeName"] = new SelectList(_context.DataDbs, "Nik", "Nik", employee.EmployeeName);
            ViewData["JobTitles"] = new SelectList(_context.JobTitles, "Id", "JobTitle1", employee.JobTitles);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.DataDb)
                .Include(e => e.JobTitle)
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
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'DataPribadiDbContext.Employees'  is null.");
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(string id)
        {
          return (_context.Employees?.Any(e => e.EmpId == id)).GetValueOrDefault();
        }
    }
}
