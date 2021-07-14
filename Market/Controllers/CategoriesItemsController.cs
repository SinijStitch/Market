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
    public class CategoriesItemsController : Controller
    {
        private readonly MarketContext _context;

        public CategoriesItemsController(MarketContext context)
        {
            _context = context;
        }

        // GET: CategoriesItems
        public async Task<IActionResult> Index()
        {
            var marketContext = _context.CategoriesItems.Include(c => c.Category).Include(c => c.Item);
            return View(await marketContext.ToListAsync());
        }
        public async Task<IActionResult> CategoriesListByItemId(int? id)
        {

            var marketContext = _context.CategoriesItems.Include(c => c.Category).Include(c => c.Item).Where(c => c.ItemId == id);
            return View(await marketContext.ToListAsync());
        }

        // GET: CategoriesItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriesItem = await _context.CategoriesItems
                .Include(c => c.Category)
                .Include(c => c.Item)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriesItem == null)
            {
                return NotFound();
            }

            return View(categoriesItem);
        }

        // GET: CategoriesItems/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name");
            return View();
        }

        // POST: CategoriesItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryId,ItemId")] CategoriesItem categoriesItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoriesItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", categoriesItem.CategoryId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", categoriesItem.ItemId);
            return View(categoriesItem);
        }

        // GET: CategoriesItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriesItem = await _context.CategoriesItems.FindAsync(id);
            if (categoriesItem == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", categoriesItem.CategoryId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", categoriesItem.ItemId);
            return View(categoriesItem);
        }

        // POST: CategoriesItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryId,ItemId")] CategoriesItem categoriesItem)
        {
            if (id != categoriesItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriesItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriesItemExists(categoriesItem.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", categoriesItem.CategoryId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", categoriesItem.ItemId);
            return View(categoriesItem);
        }

        // GET: CategoriesItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriesItem = await _context.CategoriesItems
                .Include(c => c.Category)
                .Include(c => c.Item)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriesItem == null)
            {
                return NotFound();
            }

            return View(categoriesItem);
        }

        // POST: CategoriesItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoriesItem = await _context.CategoriesItems.FindAsync(id);
            _context.CategoriesItems.Remove(categoriesItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriesItemExists(int id)
        {
            return _context.CategoriesItems.Any(e => e.Id == id);
        }
    }
}
