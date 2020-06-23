using gestao.Data;
using gestao.Data.Entities;
using gestao.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gestao.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IRepository _context;
        public AppController(IMailService mailService, IRepository context)
        {
            this._context = context;
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
            return View();
        }

    }
}