using Microsoft.AspNetCore.Mvc;

namespace DataPribadiNetCoreMVC.Controllers
{
    public class TableViewController : Controller
    {
        public DataPribadiDbContext _context = new DataPribadiDbContext();

        public IActionResult Index()
        {
            var country = _context.DataDbs.Include(d => d.Country);
            var data = country.ToList();
            var dept = _context.JobTitles.Include(d => d.Department);
            var jobtitle = dept.ToList();
            var tableVM = new tableVM
            {
                DataDbs= data,
                JobTitles= jobtitle
            };
            return View(tableVM);
        }
    }
}
