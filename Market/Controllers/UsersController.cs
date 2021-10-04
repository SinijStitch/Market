using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Market.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Lib;

namespace Market.Controllers
{
 
    
    public class UsersController : Controller
    {
        private readonly MarketContext _context;
        
        public static User ActiveUser = null;

        public UsersController(MarketContext context,  IWebHostEnvironment appEnvironment)
        {
            _context = context;
            Media._appEnvironment = appEnvironment;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }


       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        public async Task<IActionResult> ManagerDetails(int? id)
        {


            if (id == null)
            {

                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            RouteDispatcher.PreviousPage = "ManagerDetails";
            return View(user);
        }

        public async Task<IActionResult> SuperAdminCabinet(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        public async Task<IActionResult> MarketManagerCabinet(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);

            //var marketContext = _context.UsersRoles.Include(u => u.Role).Include(u => u.User).Include(u => u.Seller);

            if (user == null)
            {
                return NotFound();
            }
            // ViewData["SellerId"] = new SelectList(_context.Sellers, "Id", "Name", usersRole.SellerId);
            //ViewData["SellerId"] = new SelectList(_context.Sellers, "Id", "Name", 1);
            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            Media.Init("Users", "Create", null, null);
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Password,Login,Mail")] User user)
        {

            if (ActiveUser == null)
            {

            }
            if (ModelState.IsValid)
            {
                user.Image = Media.Image;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        // GET: Users/Create
        public IActionResult LogIn()
        {
            return View();
        }
        public IActionResult LogOut()
        {
            UsersController.ActiveUser = null;
            Models.Role.ActiveUserRole = RolesEnum.User;
            return RedirectToAction(nameof(LogIn));
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn([Bind("Password,Login")] User user)
        {
            ActiveUser = null;
            foreach (User u in _context.Users)
            {
                if (u.Login == user.Login && u.Password==user.Password)
                {
                    ActiveUser = u;
                }
            }

            if (ActiveUser != null)
            {
                var uroles = _context.UsersRoles;
                foreach (UsersRole ur in uroles)
                {
                    if (ur.UserId == ActiveUser.Id && ur.RoleId == (int)Models.RolesEnum.SuperAdmin)
                    {
                        Models.Role.ActiveUserRole = RolesEnum.SuperAdmin;
                        return RedirectToAction("SuperAdminCabinet", new { id = ActiveUser.Id });
                    }

                    if (ur.UserId == ActiveUser.Id && ur.RoleId == (int)Models.RolesEnum.MarketAdmin)
                    {
                        Models.Role.ActiveUserRole = RolesEnum.MarketAdmin;
                        // ActiveSeloler
                        //return RedirectToAction("MarketManagerCabinet", new { id = ActiveUser.Id });
                        return RedirectToAction("ManagerDetails", new { id = ActiveUser.Id });
                    }
                }
                return RedirectToAction("Details", new { id = ActiveUser.Id });

            }

            return LogIn();
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            Media.Init("Users", "Edit", user.Id, user.Image);

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Password,Login,Mail")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    user.Image = Media.Image;
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
                return RedirectToAction(RouteDispatcher.PreviousPage, new { id = id });
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
