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
using gestao.ViewModels;
using gestao.Views.App;

namespace gestao.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Chefe do Setor")]
    public class AdminFichasController : Controller
    {
        private readonly AppGestaoContext _context;
        private readonly IRepository _ctx;

        public AdminFichasController(AppGestaoContext context, IRepository ctx)
        {
            _context = context;
            _ctx = ctx;
        }

        // GET: Admin/AdminFichas
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TituloParm"] = String.IsNullOrEmpty(sortOrder) ? "titulo_desc" : "";
            ViewData["DataFichaSortParm"] = sortOrder == "Data" ? "dataFicha_desc" : "Data";
            ViewData["CurrentFilter"] = searchString;

            if(searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            var fichas = from f in _context.Fichas
                         select f;
            if(!String.IsNullOrEmpty(searchString))
            {
                fichas = fichas.Where(f => f.titulo.Contains(searchString)
                                        || f.descricao.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "titulo_desc":
                    fichas = fichas.OrderByDescending(f => f.titulo);
                    break;
                case "Data":
                    fichas = fichas.OrderBy(f => f.dataficha);
                    break;
                case "dataFicha_desc":
                    fichas = fichas.OrderByDescending(f => f.dataficha);
                    break;
                default: fichas = fichas.OrderBy(f => f.dataficha);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<Ficha>.CreateAsync(fichas.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Admin/AdminFichas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ficha = await _context.Fichas
                .FirstOrDefaultAsync(m => m.FichaId == id);
            if (ficha == null)
            {
                return NotFound();
            }

            return View(ficha);
        }

        // GET: Admin/AdminFichas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminFichas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FichaId,titulo,descricao,dataficha")] Ficha ficha)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ficha);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ficha);
        }

        // GET: Admin/AdminFichas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ficha = await _context.Fichas.FindAsync(id);
            if (ficha == null)
            {
                return NotFound();
            }
            return View(ficha);
        }

        // POST: Admin/AdminFichas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FichaId,titulo,descricao,dataficha")] Ficha ficha)
        {
            if (id != ficha.FichaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ficha);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FichaExists(ficha.FichaId))
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
            return View(ficha);
        }

        // GET: Admin/AdminFichas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ficha = await _context.Fichas
                .FirstOrDefaultAsync(m => m.FichaId == id);
            if (ficha == null)
            {
                return NotFound();
            }

            return View(ficha);
        }

        // POST: Admin/AdminFichas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ficha = await _context.Fichas.FindAsync(id);
            _context.Fichas.Remove(ficha);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FichaExists(int id)
        {
            return _context.Fichas.Any(e => e.FichaId == id);
        }

        public IActionResult FichaFuncionarios(int? id)
        {
            var ficha = _context.Fichas
                .Include(fi => fi.FichaItens)
                .ThenInclude(f => f.Funcionario)
                .FirstOrDefault(fi => fi.FichaId == id);
            if(ficha == null)
            {
                Response.StatusCode = 404;
                return View("FichaNotFound", id.Value);
            }
            FuncionarioFichaViewModel fichaFuncionarios = new FuncionarioFichaViewModel()
            {
                Ficha = ficha,
                FichaDetalhe = ficha.FichaItens
            };

            return View(fichaFuncionarios);

        }
        public async Task<IActionResult> ListaFichaFuncionarios()
        {
           
            return View();
        }

    }
}
