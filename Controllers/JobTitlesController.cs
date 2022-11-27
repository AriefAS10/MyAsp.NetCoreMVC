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
    public class JobTitlesController : Controller
    {
        private readonly DataPribadiDbContext _context;

        public JobTitlesController(DataPribadiDbContext context)
        {
            _context = context;
        }

        // GET: JobTitles
        public async Task<IActionResult> Index()
        {
            var dataPribadiDbContext = _context.JobTitles.Include(j => j.Department);
            return View(await dataPribadiDbContext.ToListAsync());
        }

        // GET: JobTitles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.JobTitles == null)
            {
                return NotFound();
            }

            var jobTitle = await _context.JobTitles
                .Include(j => j.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobTitle == null)
            {
                return NotFound();
            }

            return View(jobTitle);
        }

        // GET: JobTitles/Create
        public IActionResult Create(string JobCode)
        {
            JobTitle jb = new JobTitle();
            var Jcode = _context.JobTitles.OrderByDescending(m => m.JobCode).FirstOrDefault();
            if (JobCode != null)
            {
                jb = _context.JobTitles.Where(x => x.JobCode == JobCode).FirstOrDefault<JobTitle>();
            }
            else if (Jcode == null)
            {
                jb.JobCode = "J0001";
            }
            else
            {
                jb.JobCode = "J" + (Convert.ToInt32(Jcode.JobCode[1..]) + 1).ToString("D4");
            }
            var deptList = _context.Departments.ToList();
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "DepartmentName");
            ViewBag.Departments = deptList;
            return View(jb);
        }

        // POST: JobTitles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JobCode,JobTitle1,DepartmentId")] JobTitle jobTitle)
        {

            try
            {
                _context.Add(jobTitle);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Data Berhasil Ditambahkan";
            }
            catch (Exception Create)
            {
                ViewBag.ErrorMessage = Create.Message;
            }

            return RedirectToAction("Index");
        }


        // GET: JobTitles/MasterCreate
        public IActionResult MasterCreate()
        {
            var DeptList = _context.Departments.ToList();
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "DepartmentName");
            ViewBag.Departments = DeptList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MasterCreate([Bind("Id,JobCode,JobTitle1,DepartmentId")] JobTitle jobTitle)
        {

            try
            {
                jobTitle.JobCode = "J" + (Convert.ToInt32(jobTitle.JobCode.Substring(0, jobTitle.JobCode.Length)) + 0).ToString("D4");
                _context.Add(jobTitle);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Data Berhasil Ditambahkan";
            }
            catch (Exception MasterCreate)
            {
                ViewBag.ErrorMessage = MasterCreate.Message;
            }

            return RedirectToAction("Index");
        }

        // GET: JobTitles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.JobTitles == null)
            {
                return NotFound();
            }

            var jobTitle = await _context.JobTitles.FindAsync(id);
            if (jobTitle == null)
            {
                return NotFound();
            }
            return View(jobTitle);
        }

        // POST: JobTitles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JobCode,JobTitle1,DepartmentId")] JobTitle jobTitle)
        {
            if (id != jobTitle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobTitle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobTitleExists(jobTitle.Id))
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
            return View(jobTitle);
        }

        // GET: JobTitles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.JobTitles == null)
            {
                return NotFound();
            }

            var jobTitle = await _context.JobTitles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobTitle == null)
            {
                return NotFound();
            }

            return View(jobTitle);
        }

        // POST: JobTitles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.JobTitles == null)
            {
                return Problem("Entity set 'DataPribadiDbContext.JobTitles'  is null.");
            }
            var jobTitle = await _context.JobTitles.FindAsync(id);
            if (jobTitle != null)
            {
                _context.JobTitles.Remove(jobTitle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobTitleExists(int id)
        {
          return (_context.JobTitles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
