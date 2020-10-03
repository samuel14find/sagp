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
    public class AdminProgressoesController : Controller
    {
        private readonly AppGestaoContext _context;

        public AdminProgressoesController(AppGestaoContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminProgressoes
        public async Task<IActionResult> Index()
        {
            var appGestaoContext = _context.Progressoes.Include(p => p.Carreira);
            return View(await appGestaoContext.ToListAsync());
        }

        // GET: Admin/AdminProgressoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var progressaoCarreira = await _context.Progressoes
                .Include(p => p.Carreira)
                .FirstOrDefaultAsync(m => m.ProgressaoCode == id);
            if (progressaoCarreira == null)
            {
                return NotFound();
            }

            return View(progressaoCarreira);
        }

        // GET: Admin/AdminProgressoes/Create
        public IActionResult Create()
        {
            ViewData["Iso5"] = new SelectList(_context.Carreiras, "Iso5", "Iso5");
            return View();
        }

        // POST: Admin/AdminProgressoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProgressaoCode,NomeProgressao,Iso5")] ProgressaoCarreira progressaoCarreira)
        {
            if (ModelState.IsValid)
            {
                _context.Add(progressaoCarreira);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Iso5"] = new SelectList(_context.Carreiras, "Iso5", "Iso5", progressaoCarreira.Iso5);
            return View(progressaoCarreira);
        }

        // GET: Admin/AdminProgressoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var progressaoCarreira = await _context.Progressoes.FindAsync(id);
            if (progressaoCarreira == null)
            {
                return NotFound();
            }
            ViewData["Iso5"] = new SelectList(_context.Carreiras, "Iso5", "Iso5", progressaoCarreira.Iso5);
            return View(progressaoCarreira);
        }

        // POST: Admin/AdminProgressoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ProgressaoCode,NomeProgressao,Iso5")] ProgressaoCarreira progressaoCarreira)
        {
            if (id != progressaoCarreira.ProgressaoCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(progressaoCarreira);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgressaoCarreiraExists(progressaoCarreira.ProgressaoCode))
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
            ViewData["Iso5"] = new SelectList(_context.Carreiras, "Iso5", "Iso5", progressaoCarreira.Iso5);
            return View(progressaoCarreira);
        }

        // GET: Admin/AdminProgressoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var progressaoCarreira = await _context.Progressoes
                .Include(p => p.Carreira)
                .FirstOrDefaultAsync(m => m.ProgressaoCode == id);
            if (progressaoCarreira == null)
            {
                return NotFound();
            }

            return View(progressaoCarreira);
        }

        // POST: Admin/AdminProgressoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var progressaoCarreira = await _context.Progressoes.FindAsync(id);
            _context.Progressoes.Remove(progressaoCarreira);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgressaoCarreiraExists(string id)
        {
            return _context.Progressoes.Any(e => e.ProgressaoCode == id);
        }
    }
}
