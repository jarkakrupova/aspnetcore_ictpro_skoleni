                                                                using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;
using MVC.Services;

namespace MVC.Controllers
{

    public class NewSocksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SimpleFileLogger _simpleFileLogger;

        public NewSocksController(ApplicationDbContext context, SimpleFileLogger simpleFileLogger)
        {
            _context = context;
            _simpleFileLogger = simpleFileLogger;
        }

        // GET: NewSocks
        public async Task<IActionResult> Index()
        {
            string login = "neprihlasen";
            if (User.Identity.IsAuthenticated) {
                login = User.Identity.Name;
            }
            ViewData["login"] = login;
            _simpleFileLogger.Log("byl volan Index z NewSocksControlleru");
            return View(await _context.NewSocks.ToListAsync());
        }

        // GET: NewSocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newSocks = await _context.NewSocks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newSocks == null)
            {
                return NotFound();
            }
            _simpleFileLogger.Log("byly volany Details z NewSocksControlleru");

            return View(newSocks);
        }
        [Authorize]
        // GET: NewSocks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NewSocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Brand,Size,Price,OnStock")] NewSocks newSocks)
        {
            if (ModelState.IsValid)
            {
                _context.NewSocks.Add(newSocks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newSocks);
        }

        // GET: NewSocks/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newSocks = await _context.NewSocks.FindAsync(id);
            if (newSocks == null)
            {
                return NotFound();
            }
            return View(newSocks);
        }

        // POST: NewSocks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Brand,Size,Price,OnStock")] NewSocks newSocks)
        {
            if (id != newSocks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.NewSocks.Update(newSocks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewSocksExists(newSocks.Id))
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
            return View(newSocks);
        }

        // GET: NewSocks/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newSocks = await _context.NewSocks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newSocks == null)
            {
                return NotFound();
            }

            return View(newSocks);
        }

        // POST: NewSocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newSocks = await _context.NewSocks.FindAsync(id);
            if (newSocks != null)
            {
                _context.NewSocks.Remove(newSocks);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewSocksExists(int id)
        {
            return _context.NewSocks.Any(e => e.Id == id);
        }
    }
}
