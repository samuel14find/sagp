using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gestao.Data;
using gestao.Data.Entities;
using Microsoft.AspNetCore.Authorization;

namespace gestao.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Chefe do Setor")]
    public class AdminCarreirasController : Controller
    {
        private readonly AppGestaoContext _context;

        public AdminCarreirasController(AppGestaoContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminCarreiras
        public async Task<IActionResult> Index()
        {
            return View(await _context.Carreiras.ToListAsync());
        }

        // GET: Admin/AdminCarreiras/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carreira = await _context.Carreiras
                .FirstOrDefaultAsync(m => m.Iso5 == id);
            if (carreira == null)
            {
                return NotFound();
            }

            return View(carreira);
        }

        // GET: Admin/AdminCarreiras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminCarreiras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Iso5,NomeCarreira")] Carreira carreira)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carreira);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carreira);
        }

        // GET: Admin/AdminCarreiras/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carreira = await _context.Carreiras.FindAsync(id);
            if (carreira == null)
            {
                return NotFound();
            }
            return View(carreira);
        }

        // POST: Admin/AdminCarreiras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Iso5,NomeCarreira")] Carreira carreira)
        {
            if (id != carreira.Iso5)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carreira);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarreiraExists(carreira.Iso5))
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
            return View(carreira);
        }

        // GET: Admin/AdminCarreiras/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carreira = await _context.Carreiras
                .FirstOrDefaultAsync(m => m.Iso5 == id);
            if (carreira == null)
            {
                return NotFound();
            }

            return View(carreira);
        }

        // POST: Admin/AdminCarreiras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var carreira = await _context.Carreiras.FindAsync(id);
            _context.Carreiras.Remove(carreira);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarreiraExists(string id)
        {
            return _context.Carreiras.Any(e => e.Iso5 == id);
        }
    }
}
