using Drug.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Drug.Controllers
{
    public class BillingController : Controller
    {
        private readonly DrugDbContext _context;


        public BillingController(DrugDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return _context.BillingTables != null ?
                        View(await _context.BillingTables.ToListAsync()) :
                        Problem("Entity set 'DrugDbContext.Drugs'  is null.");
        }
    }
}
