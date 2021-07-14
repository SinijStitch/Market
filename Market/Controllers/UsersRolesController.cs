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
    public class UsersRolesController : Controller
    {
        private readonly MarketContext _context;

        public UsersRolesController(MarketContext context)
        {
            _context = context;
        }

        // GET: UsersRoles
        public async Task<IActionResult> Index()
        {
            var marketContext = _context.UsersRoles.Include(u => u.Role).Include(u => u.User).Include(u => u.Seller);
            return View(await marketContext.ToListAsync());
        }

        // GET: UsersRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersRole = await _context.UsersRoles
                .Include(u => u.Role)
                .Include(u => u.Seller)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usersRole == null)
            {
                return NotFound();
            }

            return View(usersRole);
        }

        // GET: UsersRoles/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            ViewData["SellerId"] = new SelectList(_context.Sellers, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: UsersRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,RoleId,SellerId")] UsersRole usersRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usersRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", usersRole.RoleId);
            ViewData["SellerId"] = new SelectList(_context.Sellers, "Id", "Name", usersRole.SellerId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", usersRole.UserId);
            return View(usersRole);
        }

        // GET: UsersRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersRole = await _context.UsersRoles.FindAsync(id);
            if (usersRole == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", usersRole.RoleId);
            ViewData["SellerId"] = new SelectList(_context.Sellers, "Id", "Name", usersRole.SellerId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", usersRole.UserId);
            return View(usersRole);
        }

        // POST: UsersRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,RoleId,SellerId")] UsersRole usersRole)
        {
            if (id != usersRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usersRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersRoleExists(usersRole.Id))
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
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", usersRole.RoleId);
            ViewData["SellerId"] = new SelectList(_context.Sellers, "Id", "Name", usersRole.SellerId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", usersRole.UserId);
            return View(usersRole);
        }

        // GET: UsersRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersRole = await _context.UsersRoles
                .Include(u => u.Role)
                .Include(u => u.Seller)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usersRole == null)
            {
                return NotFound();
            }

            return View(usersRole);
        }

        // POST: UsersRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usersRole = await _context.UsersRoles.FindAsync(id);
            _context.UsersRoles.Remove(usersRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersRoleExists(int id)
        {
            return _context.UsersRoles.Any(e => e.Id == id);
        }
    }
}
