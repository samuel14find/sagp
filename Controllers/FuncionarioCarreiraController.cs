using System.Collections.Generic;
using gestao.Data;
using gestao.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using gestao.Data.Entities;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using gestao.Views.App;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace gestao.Controllers
{
    //  Comentários
    // Aqui vou retornar todos os funcionarios com suas carreiras
    // Como o controlador passa informação para view ? Passo usando uma view 
    // fortemente tipada, como eu fiz aqui, ou posso usar o ViewBag, fazendo assim
    // ViewBag.Funcionarios = " Funcionarios". Ou posso usar um ViewData fazendo 
    // assim ViewData["Funcionarios"] = "Funcionarios". 

    [Authorize(Roles = "Chefe do Setor, Assistente do Setor")]
    public class FuncionarioCarreiraController : Controller
    {
        private readonly IFuncionarioRepository _repo;
        private readonly IProgressoesRepository _progre;
        private readonly AppGestaoContext _context;
        private readonly ILogger<FuncionarioCarreiraController> _logger;

        public FuncionarioCarreiraController(IFuncionarioRepository repo,
                                             IProgressoesRepository progre,
                                             AppGestaoContext context,
                                             ILogger<FuncionarioCarreiraController> logger)
        {
            this._context = context;
            _logger = logger;
            this._repo = repo;
            _progre = progre;
        }

        [Authorize]
        public async Task<IActionResult> Index(string porCarreira, string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TituloParm"] = String.IsNullOrEmpty(sortOrder) ? "nome_desc" : "";
            ViewData["CurrentFilter"] = searchString;
            string _porCarreira = porCarreira;
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            //IEnumerable<FuncionarioCarreira> funcionarios = new List<FuncionarioCarreira>();
            var funcionarios = from f in _context.FuncionariosCarreira
                               select f;
            string carreiraAtual = string.Empty;
            
            if (string.IsNullOrEmpty(porCarreira))
            {
                funcionarios = funcionarios
                    .OrderBy(f => f.NomeFuncionario);
                if (!String.IsNullOrEmpty(searchString))
                {
                    funcionarios = funcionarios
                    .Where(f=>f.NomeFuncionario.Contains(searchString) || f.CarreiraIso5.Contains(searchString) || f.ProgressaoCode.Contains(searchString))
                    .OrderBy(f => f.NomeFuncionario);
                }
                porCarreira = "Todos os Funcionarios";
            }
            else
            {
                if( string.Equals("Admin", _porCarreira, StringComparison.OrdinalIgnoreCase))
                {
                    funcionarios = funcionarios.Where(f =>
                     f.CarreiraIso5.Equals("Admin")).OrderBy(f => f.NomeFuncionario);
                    if(!String.IsNullOrEmpty(searchString))
                    {
                        funcionarios = funcionarios.Where(f =>
                        f.CarreiraIso5.Equals("Admin") &&
                        f.NomeFuncionario.Contains(searchString) || f.CarreiraIso5.Contains(searchString) || f.ProgressaoCode.Contains(searchString)).OrderBy(f => f.NomeFuncionario);
                    }
                } 
                else if (string.Equals("Prof", _porCarreira, StringComparison.OrdinalIgnoreCase)) {
                   funcionarios = funcionarios.Where(f =>
                     f.CarreiraIso5.Equals("Prof")).OrderBy(f => f.NomeFuncionario); 
                    if(!String.IsNullOrEmpty(searchString))
                    {
                        funcionarios = funcionarios.Where(f =>
                        f.CarreiraIso5.Equals("Prof") &&
                        f.NomeFuncionario.Contains(searchString) || f.CarreiraIso5.Contains(searchString) || f.ProgressaoCode.Contains(searchString)).OrderBy(f => f.NomeFuncionario);
                    }
                }
                else if(string.Equals("Terc", _porCarreira, StringComparison.OrdinalIgnoreCase))
                {
                    funcionarios = funcionarios.Where(f =>
                     f.CarreiraIso5.Equals("Terc")).OrderBy(f => f.NomeFuncionario);
                    if (!String.IsNullOrEmpty(searchString))
                    {
                        funcionarios = funcionarios.Where(f =>
                        f.CarreiraIso5.Equals("Terc") &&
                        f.NomeFuncionario.Contains(searchString) || f.CarreiraIso5.Contains(searchString) || f.ProgressaoCode.Contains(searchString)).OrderBy(f => f.NomeFuncionario);
                    }
                    
                }

                
                carreiraAtual = _porCarreira;
            }
            switch (sortOrder)
            {
                case "nome_desc":
                    funcionarios = funcionarios.OrderByDescending(f => f.NomeFuncionario);
                    break;
                default:
                    funcionarios = funcionarios.OrderBy(f => f.NomeFuncionario);
                    break;
            }

            //List<FuncionarioCarreiraDisplayViewModel> funcionariosDisplay = new List<FuncionarioCarreiraDisplayViewModel>();
            //foreach(var x in funcionarios)
            //{
            //    var funcionarioDisplay = new FuncionarioCarreiraDisplayViewModel()
            //    {
            //        IdentificadorFuncionarioCarreira = x.IdentificadorFuncionarioCarreira,
            //        NomeFuncionario = x.NomeFuncionario,
            //        NomeCarreira = x.CarreiraIso5,
            //        NomeProgressao = x.ProgressaoCode
            //    };
            //    funcionariosDisplay.Add(funcionarioDisplay);
            //};
            int pageSize = 15;
                return View(await PaginatedList<FuncionarioCarreira>.CreateAsync(funcionarios.AsNoTracking(), pageNumber ?? 1, pageSize));
                //return View(await PaginatedList<Ficha>.CreateAsync(fichas.AsNoTracking(), pageNumber ?? 1, pageSize));
           
           
            //return View();
        }

        [HttpGet]
        
        public IActionResult GetProgressoes(string iso5)
        {
            if (!(string.IsNullOrWhiteSpace(iso5) && iso5.Length == 5)) // && iso5.Length == 5
            {
                
                IEnumerable<SelectListItem> progressoes = _progre.GetProgressoesComCarreira(iso5);
                return Json(progressoes);
            }
            return null;
        }

        // Get: FuncionarioCarreira/Create
        [Authorize]
        public ActionResult Create()
        {

            var funcionarioFicha = _repo.CreateFuncionarioCarreira();
            return View(funcionarioFicha);
        }

        [HttpPost]
        public IActionResult Create([Bind("IdentificadorFuncionarioCarreira, NomeFuncionario, SelectedCarreiraIso5, SelectedProgressaoCode")] FuncionarioEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    bool saved = _repo.SaveFuncionarioCarreira(model);
                    if (saved)
                    {
                        _logger.LogInformation($"Carreira do {model.NomeFuncionario} criada");
                        return RedirectToAction("Index");
                    }

                }
                else
                {
                    return View("Error", model);
                }
            }
            catch (ApplicationException ex)
            {
                _logger.LogError($"Erro ao criar carreira do funcionario {ex}");
                
            }
            return View("Error", model);
        }

        public async Task<IActionResult> Delete(Guid? Id)
        {
            if(Id == null)
            {
                View("ErrorDelete", Id.Value);
            }
            var carreira = await _context.FuncionariosCarreira
                .FirstOrDefaultAsync(c => c.IdentificadorFuncionarioCarreira == Id);
            if(carreira == null)
            {
                View("ErrorDelete", Id.Value);
            }
            return View(carreira);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid Id)
        {
            var carreira = await _context.FuncionariosCarreira.FindAsync(Id);
            _context.FuncionariosCarreira.Remove(carreira);
            _logger.LogInformation($"A carreira de Id {Id} do {carreira.NomeFuncionario} foi deletada");
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Create), 302);
        }
    }
}