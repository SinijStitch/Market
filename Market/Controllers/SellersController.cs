using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Market.Models;

namespace Market.Controllers
{
    public class SellersController : Controller
    {
        private readonly MarketContext _context;
        public static Seller ActiveSeller = null;
        public SellersController(MarketContext context)
        {
            _context = context;
        }

  
        // GET: Sellers
        public async Task<IActionResult> Index()
        {
            if (Models.Role.ActiveUserRole != Models.RolesEnum.SuperAdmin)
            {
                return RedirectToAction("MessageBox", "Home", new { msg = "405" }, null);
            }

            return View(await _context.Sellers.ToListAsync());
        }

        public async Task<IActionResult> ShopsForUser()
        {
           

            return View(await _context.Sellers.ToListAsync());
        }

        public async Task<IActionResult> MarketsListByManagerId()
        { 


            if (Models.Role.ActiveUserRole != Models.RolesEnum.MarketAdmin)
            {
                return RedirectToAction("MessageBox", "Home", new { msg = "You are not administrator" }, null);
            }

            //var markets = _context.Sellers.Include
            var marketContext = _context.UsersRoles.Include(u => u.Role).Include(u => u.User).Include(u => u.Seller).Where(u=>u.UserId == UsersController.ActiveUser.Id);

            return View(await marketContext.ToListAsync());
        }

        // GET: Sellers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (Models.Role.ActiveUserRole != Models.RolesEnum.SuperAdmin &&  Models.Role.ActiveUserRole != Models.RolesEnum.MarketAdmin)
            {
                return RedirectToAction("MessageBox", "Home", new { msg = "405" }, null);
            }
            if (id == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // GET: Sellers/Create
        public IActionResult Create()
        {
            if (Models.Role.ActiveUserRole != Models.RolesEnum.SuperAdmin)
            {
                return RedirectToAction("MessageBox", "Home", new { msg = "405" }, null);
            }
            return View();
        }

        // POST: Sellers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Image")] Seller seller)
        {
            if (Models.Role.ActiveUserRole != Models.RolesEnum.SuperAdmin)
            {
                return RedirectToAction("MessageBox", "Home", new { msg = "405" }, null);
            }
            if (ModelState.IsValid)
            {
                _context.Add(seller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seller);
        }

        // GET: Sellers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (Models.Role.ActiveUserRole != Models.RolesEnum.SuperAdmin && Models.Role.ActiveUserRole != Models.RolesEnum.MarketAdmin)
            {
                return RedirectToAction("MessageBox", "Home", new { msg = "405" }, null);
            }
            if (id == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers.FindAsync(id);
            if (seller == null)
            {
                return NotFound();
            }
            return View(seller);
        }

        // POST: Sellers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Image")] Seller seller)
        {
            if (Models.Role.ActiveUserRole != Models.RolesEnum.SuperAdmin && Models.Role.ActiveUserRole != Models.RolesEnum.MarketAdmin)
            {
                return RedirectToAction("MessageBox", "Home", new { msg = "405" }, null);
            }
            if (id != seller.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seller);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SellerExists(seller.Id))
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
            return View(seller);
        }

        // GET: Sellers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (Models.Role.ActiveUserRole != Models.RolesEnum.SuperAdmin && Models.Role.ActiveUserRole != Models.RolesEnum.MarketAdmin)
            {
                return RedirectToAction("MessageBox", "Home", new { msg = "405" }, null);
            }
            if (id == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // POST: Sellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (Models.Role.ActiveUserRole != Models.RolesEnum.SuperAdmin && Models.Role.ActiveUserRole != Models.RolesEnum.MarketAdmin)
            {
                return RedirectToAction("MessageBox", "Home", new { msg = "405" }, null);
            }
            var seller = await _context.Sellers.FindAsync(id);
            _context.Sellers.Remove(seller);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SellerExists(int id)
        {

            return _context.Sellers.Any(e => e.Id == id);
        }
    }
}
