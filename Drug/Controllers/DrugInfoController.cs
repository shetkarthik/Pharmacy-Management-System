using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Drug.Models;
using System.Security.Cryptography.X509Certificates;
using Drug.Data;

namespace Drug.Controllers
{
    public class DrugInfoController : Controller
    {
        private readonly DrugDbContext _context;
        public Boolean IsExist;

        public DrugInfoController(DrugDbContext context)
        {
            _context = context;
        }

        
        // GET: DrugInfo
        public async Task<IActionResult> Index()
        {
              return _context.Drugs != null ? 
                          View(await _context.Drugs.ToListAsync()) :
                          Problem("Entity set 'DrugDbContext.Drugs'  is null.");
        }

        // GET: DrugInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Drugs == null)
            {
                return NotFound();
            }

            var drugInfo = await _context.Drugs
                .FirstOrDefaultAsync(m => m.DrugId == id);
            if (drugInfo == null)
            {
                return NotFound();
            }

            return View(drugInfo);
        }

        // GET: DrugInfo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DrugInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DrugId,DrugName,DrugGenericName,Quantity,DrugImage,Price,ExpiryDate,DosageForm")] DrugInfo drugInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(drugInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(drugInfo);
        }

        // GET: DrugInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Drugs == null)
            {
                return NotFound();
            }

            var drugInfo = await _context.Drugs.FindAsync(id);
            if (drugInfo == null)
            {
                return NotFound();
            }
            return View(drugInfo);
        }

        // POST: DrugInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DrugId,DrugName,DrugGenericName,Quantity,DrugImage,Price,ExpiryDate,DosageForm")] DrugInfo drugInfo)
        {
            if (id != drugInfo.DrugId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drugInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrugInfoExists(drugInfo.DrugId))
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
            return View(drugInfo);
        }

        // GET: DrugInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Drugs == null)
            {
                return NotFound();
            }

            var drugInfo = await _context.Drugs
                .FirstOrDefaultAsync(m => m.DrugId == id);
            if (drugInfo == null)
            {
                return NotFound();
            }

            return View(drugInfo);
        }

        // POST: DrugInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Drugs == null)
            {
                return Problem("Entity set 'DrugDbContext.Drugs'  is null.");
            }
            var drugInfo = await _context.Drugs.FindAsync(id);
            if (drugInfo != null)
            {
                _context.Drugs.Remove(drugInfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrugInfoExists(int id)
        {
          return (_context.Drugs?.Any(e => e.DrugId == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> ViewProducts()
        {
            var drugs = await _context.Drugs.ToListAsync();
            return View(drugs);
        }

        public async Task<Boolean> CheckDrugExistsInCart(int drugId)
        {
            var drugExistsInCart = await _context.Carts.FirstOrDefaultAsync(x => x.DrugId == drugId);

            if (drugExistsInCart == null)
            {
                return false;
            }

            return true;
        }

        public async Task<int> CheckQuantityofDrugInCart(int drugId)
        {
            var cartItem = await _context.Carts.FirstOrDefaultAsync(x => x.DrugId == drugId);

            // If the drug is not in the cart, return 0 or any other default value
            if (cartItem == null)
            {
                return 0;
            }

            return cartItem.Quantity;
        }


    }
}
