using Drug.Data;
using Drug.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Drug.Controllers
{
    public class CartController : Controller
    {
        private readonly DrugDbContext _context;


        public CartController(DrugDbContext context)
        {
            _context = context;
        }
        // GET: DrugInfo
        public async Task<IActionResult> Index()
        {
            var carts = await _context.Carts.ToListAsync();

            if (carts != null && carts.Any())
            {
                return View(carts);
            }
            else
            {
                Console.WriteLine("****");
                return View("CartEmpty");
            }
        }





        [HttpPost]
        public async Task<IActionResult> AddToCart(int drugId,int quantity)
        {
           var drugExistsInCart = await _context.Carts.FirstOrDefaultAsync(x => x.DrugId == drugId);

            var drugExistsInDB = await _context.Drugs.FirstOrDefaultAsync(x => x.DrugId == drugId);

            if (drugExistsInCart == null) 
            {
                CartInfo cart = new()
                {
                    DrugId = drugId,
                    DosageForm = drugExistsInDB.DosageForm,
                    Quantity = quantity,
                    DrugName = drugExistsInDB.DrugName,
                    Price = drugExistsInDB.Price,
                    DrugGenericName = drugExistsInDB.DrugGenericName,
                    DrugImage = drugExistsInDB.DrugImage,
                    ExpiryDate = drugExistsInDB.ExpiryDate,
                    TotalCost = (float)(quantity * drugExistsInDB.Price),
                };

                _context.Add(cart);
                await _context.SaveChangesAsync();
            }
            else
            {
                drugExistsInCart.Quantity = quantity;
                drugExistsInCart.TotalCost = (float)(quantity * drugExistsInDB.Price);
                await _context.SaveChangesAsync();
            }



            return RedirectToAction("ViewProducts", "DrugInfo");

        }


        [HttpPost]
        public async Task<IActionResult> GenerateBill()
        {
            List<CartInfo> cartInfos = new List<CartInfo>();
            double totalBilledAmount = 0;
            string drugIds = "";
            string quantities = "";

            cartInfos.AddRange(await _context.Carts.ToListAsync());

            foreach (var cart in cartInfos)
            {
                var drugFromInventory = _context.Drugs.FirstOrDefault(x => x.DrugId == cart.DrugId);

                drugFromInventory.Quantity -= cart.Quantity;

                drugIds += cart.DrugId;

                if (cart != cartInfos.Last())
                {
                    drugIds += ",";
                }

                quantities += cart.Quantity;

                if (cart != cartInfos.Last())
                {
                    quantities += ",";
                }

                totalBilledAmount += cart.TotalCost;
            }

            var newbill = new BillingTable
            {
                DrugIds = drugIds,
                Quantities = quantities,
                TotalCost = totalBilledAmount,
            };

         
            _context.Add(newbill);


            _context.Carts.RemoveRange(cartInfos);

            _context.SaveChanges();

            return RedirectToAction("ViewProducts", "DrugInfo");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Drugs == null)
            {
                return Problem("Entity set 'DrugDbContext.Cart'  is null.");
            }
            var cartInfo = await _context.Carts.FindAsync(id);
            if (cartInfo != null)
            {
                _context.Carts.Remove(cartInfo);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Cart");
        }





    }
}
