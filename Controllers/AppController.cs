using gestao.Data;
using gestao.Data.Entities;
using gestao.Service;
using gestao.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gestao.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IRepository _context;
        private readonly IFuncionarioRepository _ctx;

        public AppController(IMailService mailService, IRepository context, IFuncionarioRepository ctx)
        {
            this._context = context;
            this._ctx = ctx;
            this._mailService = mailService;

        }
        public IActionResult Index()
        {
            ViewBag.Title = "Home";
            return View();
        }
        [Authorize]
        [HttpGet("Cadastro")]
        public IActionResult Cadastro()
        {

            return View();
        }
        [Authorize]
        [HttpPost("Cadastro")]
        [ValidateAntiForgeryToken]
        public IActionResult Cadastro([Bind("matricula,nome,cargo,setor,email,datanomeacao,dataposse,dataexercicio")] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcionario);
                _context.Commit();
                _mailService.SendMessage($"O funcionário {funcionario.nome}, cuja matrícula é {funcionario.matricula}, e o email {funcionario.email} foi cadastrado no sistema. Entrou em exercicio em {funcionario.dataexercicio}");
                ViewBag.UserMessage = "Usuário Cadastrado com Sucesso!";
                ModelState.Clear();
                //return RedirectToAction(nameof(Index));
            }
            else {
                //verificar erros
            }
            return View();
        }

        [Authorize]
        public IActionResult ListarFuncionarios()
        {
            ViewBag.Title = "Lista dos Funcionários";
            var listaFuncionarios =  new ListaFuncionarioViewModel();
            listaFuncionarios.Funcionarios = _context.GetFuncionarios(false);
            return View(listaFuncionarios);
        }

        public IActionResult Detalhe(int funcionarioId)
        {
            var funcionario = _context.GetFuncionarioPorId(funcionarioId);
            if(funcionario == null)
            {
                return View("~/Views/Error/Error.cshtml");
            }
            var funcionarioCarreira = _ctx.GetFuncionarioByName(funcionario.nome);
            if(funcionarioCarreira == null)
            {
                return View("~/Views/Error/Error.cshtml");
            }
            var funcionarioDetalhesViewModel = new FuncionarioDetalhesViewModel()
            {
                FuncionarioId = funcionario.FuncionarioId,
                matricula = funcionario.matricula,
                nome = funcionario.nome,
                cargo = funcionario.cargo,
                setor = funcionario.setor,
                email = funcionario.email,
                datanomeacao = funcionario.datanomeacao,
                dataposse = funcionario.dataposse,
                dataexercicio = funcionario.dataexercicio,
                IdentificadorFuncionarioCarreira = funcionarioCarreira.IdentificadorFuncionarioCarreira,
                NomeCarreira = funcionarioCarreira.NomeCarreira,
                NomeProgressao = funcionarioCarreira.NomeProgressao
            };

            return View(funcionarioDetalhesViewModel);
        }

        

    }
}