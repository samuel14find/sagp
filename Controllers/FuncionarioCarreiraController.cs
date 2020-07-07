using System.Collections.Generic;
using gestao.Data;
using gestao.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using gestao.Data.Entities;
using System.Linq;

namespace gestao.Controllers
{
    //  Comentários
    // Aqui vou retornar todos os funcionarios com suas carreiras
    // Como o controlador passa informação para view ? Passo usando uma view 
    // fortemente tipada, como eu fiz aqui, ou posso usar o ViewBag, fazendo assim
    // ViewBag.Funcionarios = " Funcionarios". Ou posso usar um ViewData fazendo 
    // assim ViewData["Funcionarios"] = "Funcionarios". 
    public class FuncionarioCarreiraController : Controller
    {
        private readonly IFuncionarioRepository _repo;
        private readonly AppGestaoContext _context;
        public FuncionarioCarreiraController(IFuncionarioRepository repo, AppGestaoContext context)
        {
            this._context = context;
            this._repo = repo;

        }
        // public IActionResult ListaFuncionarioAdmin()
        // {
        //     // var funcionarios = _context.GetFuncionarioAdm();
        //     // return View(funcionarios);
        //     ViewBag.Title = "Lista dos funcionários administrativos";
        //     var listaFuncionarioViewModel = new ListaFuncAdminViewModel();
        //     listaFuncionarioViewModel.Funcionarios = _context.GetFuncionarioAdm();
        //     // listaFuncionarioViewModel.NivelCapacitacao = "1";
        //     // listaFuncionarioViewModel.NivelClassificacao = "D";
        //     // listaFuncionarioViewModel.NivelProgressao = "01";
        //     return View(listaFuncionarioViewModel);

        // }

        // public IActionResult ListaFuncionarioProf()
        // {
        //     var funcionarios = _context.GetFuncionarioProf();
        //     return View (funcionarios);
        // }

        public ActionResult Index(string porCarreira)
        {
            string _porCarreira = porCarreira;
            IEnumerable<FuncionarioCarreira> funcionarios = new List<FuncionarioCarreira>();
            string carreiraAtual = string.Empty;
            if (string.IsNullOrEmpty(porCarreira))
            {
                funcionarios = _context.FuncionariosCarreira.OrderBy(f => f.NomeFuncionario).ToList();
                porCarreira = "Todos os Funcionarios";
            }
            else
            {
                if( string.Equals("admin", _porCarreira, StringComparison.OrdinalIgnoreCase))
                {
                    funcionarios = _context.FuncionariosCarreira.Where(f =>
                     f.CarreiraIso5.Equals("admin")).OrderBy(f => f.NomeFuncionario);
                } 
                else{
                   funcionarios = _context.FuncionariosCarreira.Where(f =>
                     f.CarreiraIso5.Equals("profe")).OrderBy(f => f.NomeFuncionario); 
                }
                carreiraAtual = _porCarreira;
            }
            if(funcionarios != null)
            {
                List<FuncionarioCarreiraDisplayViewModel> funcionariosDisplay = new List<FuncionarioCarreiraDisplayViewModel>();
                foreach(var x in funcionarios)
                {
                    var funcionarioDisplay = new FuncionarioCarreiraDisplayViewModel()
                    {
                        IdentificadorFuncionarioCarreira = x.IdentificadorFuncionarioCarreira,
                        NomeFuncionario = x.NomeFuncionario,
                        NomeCarreira = x.CarreiraIso5,
                        NomeProgressao = x.ProgressaoCode
                    };
                    funcionariosDisplay.Add(funcionarioDisplay);
                };
                return View(funcionariosDisplay);
            }
           
            return View();
        }

        [HttpGet]
        public IActionResult GetProgressoes(string iso5)
        {
            if (!(string.IsNullOrWhiteSpace(iso5) && iso5.Length == 5)) // && iso5.Length == 5
            {
                var repo = new ProgressoesRepository();
                IEnumerable<SelectListItem> progressoes = repo.GetProgressoesComCarreira(iso5);
                return Json(progressoes);
            }
            return null;
        }

        // Get: FuncionarioCarreira/Create
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
                        return RedirectToAction("Index");
                    }
                    //throw new ApplicationException("Dados Inválidos");

                }
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            return null;
        }
    }
}