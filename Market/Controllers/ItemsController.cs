using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Market.Models;
using Lib;
using Microsoft.AspNetCore.Http;

namespace Market.Controllers
{

     public class ItemsController : Controller
    {
        private readonly MarketContext _context;
       
        public static Item ActiveItem = null;
        public ItemsController(MarketContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            var marketContext = _context.Items.Include(i => i.Manufacturer).Include(i => i.Seller);
            return View(await marketContext.ToListAsync());
        }
        
        public async Task<IActionResult> ItemsListBySellerId(int? id)
        {
            SellersController.ActiveSeller = _context.Sellers.Find(id);
            var marketContext = _context.Items.Include(i => i.Manufacturer).Include(i => i.Seller).Where(i => i.SellerId ==id);
            return View(await marketContext.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Manufacturer)
                .Include(i => i.Seller)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {

            Media.Init("Items", "Create", null, null);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name");
           
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,ManufacturerId")] Item item)
        {
            if (ModelState.IsValid)
            {
                item.Image = Media.Image;
                item.SellerId = SellersController.ActiveSeller.Id;

                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }



        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var item = await _context.Items.FindAsync(id);
            Media.Init("Items", "Edit", item.Id, item.Image);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", item.ManufacturerId);
            ViewData["SellerId"] = new SelectList(_context.Sellers, "Id", "Name", item.SellerId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,ManufacturerId")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    item.SellerId = SellersController.ActiveSeller.Id;
                    item.Image = Media.Image;
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
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
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", item.ManufacturerId);
            ViewData["SellerId"] = new SelectList(_context.Sellers, "Id", "Name", item.SellerId);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Manufacturer)
                .Include(i => i.Seller)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
