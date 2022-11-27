namespace DataPribadiNetCoreMVC.Controllers
{
    public class DataDbsController : Controller
    {
        private readonly DataPribadiDbContext _context;

        public DataDbsController(DataPribadiDbContext context)
        {
            _context = context;
        }

        // GET: DataDbs
        public ActionResult Index()
        {
            var dataPribadiDbContext = _context.DataDbs.Include(d => d.Country);
            return View(dataPribadiDbContext.ToList());
        }

        // GET: DataDbs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.DataDbs == null)
            {
                return NotFound();
            }

            var dataDb = await _context.DataDbs
                .Include(d => d.Country)
                .FirstOrDefaultAsync(m => m.Nik == id);
            if (dataDb == null)
            {
                return NotFound();
            }

            return View(dataDb);
        }
        
        // GET: DataDbs/Create
        public IActionResult Create()
        {
            var countryList = _context.Countries.ToList();
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "Negara");
            ViewBag.Country = countryList;
            return View();
        }

        // POST: DataDbs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nik,NamaLengkap,JenisKelamin,TanggalLahir,Alamat,CountryId")] DataDb dataDb)
        {
            try
            {
                 _context.Add(dataDb);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Data Berhasil Ditambahkan";
            }
            catch (Exception ex)
            {
                _context.DataDbs.Where(x => x.Nik.Equals(x));
                ViewBag.Error = ex.Message;
                TempData["error"] = "NIK Telah Terdaftar";
                return RedirectToAction("Create");
            }
            return RedirectToAction("Index");
        }        

        // GET: DataDbs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.DataDbs == null)
            {
                return NotFound();
            }

            var dataDb = await _context.DataDbs.FindAsync(id);
            if (dataDb == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "Negara", dataDb.CountryId);
            return View(dataDb);
        }

        // POST: DataDbs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Nik,NamaLengkap,JenisKelamin,TanggalLahir,Alamat,CountryId")] DataDb dataDb)
        {
            if (id != dataDb.Nik)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dataDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataDbExists(dataDb.Nik))
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
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryId", dataDb.CountryId);
            return View(dataDb);
        }

        // GET: DataDbs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.DataDbs == null)
            {
                return NotFound();
            }

            var dataDb = await _context.DataDbs
                .Include(d => d.Country)
                .FirstOrDefaultAsync(m => m.Nik == id);
            if (dataDb == null)
            {
                return NotFound();
            }

            return View(dataDb);
        }

        // POST: DataDbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.DataDbs == null)
            {
                return Problem("Entity set 'DataPribadiDbContext.DataDbs'  is null.");
            }
            var dataDb = await _context.DataDbs.FindAsync(id);
            if (dataDb != null)
            {
                _context.DataDbs.Remove(dataDb);
            }
            
            await _context.SaveChangesAsync();
            TempData["AlertMessage"] = "Data Berhasil Dihapus";
            return RedirectToAction(nameof(Index));
        }

        private bool DataDbExists(string id)
        {
          return (_context.DataDbs?.Any(e => e.Nik == id)).GetValueOrDefault();
        }
    }
}
