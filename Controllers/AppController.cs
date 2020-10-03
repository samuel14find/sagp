using gestao.Data;
using gestao.Data.Entities;
using gestao.Service;
using gestao.ViewModels;
using gestao.Views.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace gestao.Controllers
{
   
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IRepository _context;
        private readonly IFuncionarioRepository _ctx;
        private readonly AppGestaoContext _appGestaoContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AppController> _logger;
        

        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public AppController(IMailService mailService, IRepository context,
                            IFuncionarioRepository ctx, AppGestaoContext appGestaoContext,
                            IHttpContextAccessor httpContextAccessor,
                            ILogger<AppController> logger
                            )
        {
            this._context = context;
            this._ctx = ctx;
            _appGestaoContext = appGestaoContext;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            this._mailService = mailService;

        }
        [Authorize(Roles = "Chefe do Setor, Assistente do Setor, Geral")]
        public IActionResult Index()
        {
            ViewBag.Title ="Sistema auxiliar do Gestão de Pessoas";
            var home = new HomePageViewModel()
            {
               Tarefas = _appGestaoContext.Tarefas.OrderByDescending(x => x.DataCriacao).Take(5)
            };
            return View(home);
        }

        [Authorize(Roles = "Chefe do Setor, Assistente do Setor")]
        [HttpGet("Cadastro")]
        public IActionResult Cadastro()
        {

            return View();
        }

        [Authorize(Roles = "Chefe do Setor, Assistente do Setor")]
        [HttpPost("Cadastro")]
        [ValidateAntiForgeryToken]
        public IActionResult Cadastro([Bind("matricula,nome,cargo,setor,email,datanomeacao,dataposse,dataexercicio")] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcionario);
                _context.Commit();
                
                _logger.LogInformation($"O usuário {funcionario.nome}, cuja matrícula é {funcionario.matricula} foi cadastrado ");
                ViewBag.UserMessage = "Usuário Cadastrado com Sucesso!";
                
                ModelState.Clear();
               
                //return RedirectToAction(nameof(Index));
            }
            else {
                //verificar erros
                ViewBag.UserMessage = "Algo está Errado!";
                _logger.LogError($"Erro ao tentar cadastrar o funcionario {funcionario.nome}. Dados inválidos {ModelState.Values} ");
            }
            return View();
        }

        [Authorize(Roles = "Chefe do Setor, Assistente do Setor")]
        public async Task<IActionResult> ListarFuncionarios(string searchString, string filtroAtual, int? pageNumber)
        {
            
            ViewBag.Title = "Lista dos Funcionários";
            
            //var listaFuncionarios =  new ListaFuncionarioViewModel();

            if(searchString != null)
            {

                pageNumber = 1;

            }
            else
            {
                searchString = filtroAtual;
            }

            ViewData["FiltroAplicado"] = searchString;
            var listaFuncionarios = from f in _appGestaoContext.Funcionarios
                                    select f;

            if (!String.IsNullOrEmpty(searchString))
            {
                listaFuncionarios = _appGestaoContext.Funcionarios.Where(f => f.nome.Contains(searchString)
                                                                                        || f.matricula.Contains(searchString) || f.setor.Contains(searchString));
                _logger.LogInformation($"Applicando o filtro: {searchString}");
            }

            int pageSize = 15;
            _logger.LogInformation($"Acessando a lista de todos os funcionarios");
            return View(await PaginatedList<Funcionario>.CreateAsync(listaFuncionarios.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        [Authorize(Roles = "Chefe do Setor, Assistente do Setor")]
        public IActionResult Detalhe(int? funcionarioId)
        {
            var funcionario = _context.GetFuncionarioPorId(funcionarioId.Value);
            var dataEntradaExercicio = _context.GetDataExercicio(funcionarioId.Value);
            var dataFimExercicioAdministrativo = dataEntradaExercicio.AddMonths(270);
            var dataFimExercicioProfessor = dataEntradaExercicio.AddMonths(288);
            _logger.LogInformation($"Acessando os detalhes do funcionario {funcionarioId}");
            if (funcionario == null|| dataEntradaExercicio == null)
            {

                _logger.LogError($"Falha na chamda do controller Detalhe. Funcionario {funcionarioId} e nulo ou data entrata exercicio não existe");
                Response.StatusCode = 404;
                return View("Error", funcionarioId.Value);
            }
            var funcionarioCarreira = _ctx.GetFuncionarioByName(funcionario.nome);
            if(funcionarioCarreira == null)
            {
                _logger.LogError($"Falha na chamda do controller Detalhe. Funcionario {funcionarioId} não tem carreira cadastrada");
                Response.StatusCode = 404;
                return View("Error", funcionarioId.Value);
            }
             IEnumerable<DateTimeOffset> progressaoPorMerito(DateTimeOffset from, DateTimeOffset thru)
            {
                for (var day = from.Date; day.Date <= thru.Date; day = day.AddMonths(18))
                    yield return day;
            }
            IEnumerable<DateTimeOffset> progressaoFuncional(DateTimeOffset from, DateTimeOffset thru)
            {
                for (var day = from.Date; day.Date <= thru.Date; day = day.AddMonths(24))
                    yield return day;
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
                NomeProgressao = funcionarioCarreira.NomeProgressao,
                progressaoFuncional = progressaoFuncional(dataEntradaExercicio, dataFimExercicioProfessor),
                progressaoPorMerito = progressaoPorMerito(dataEntradaExercicio, dataFimExercicioAdministrativo)
            };
            _logger.LogInformation($"Acessando os detalhes do funcionario {funcionarioDetalhesViewModel.nome}");
            return View(funcionarioDetalhesViewModel);
        }

        [Authorize(Roles = "Chefe do Setor, Assistente do Setor")]
        [HttpGet]
        public async Task<IActionResult> EditarFuncionario(int? Id)
        {
            if(Id == null)
            {
                _logger.LogError($"Tentando editar funcionario, mas recebi id nulo");
                return View("Error", Id.Value);
            }
            var funcionario = await _appGestaoContext.Funcionarios.FindAsync(Id);
            if(funcionario == null)
            {
                _logger.LogError($"Erro ao buscar funcionario para editar.");
                return View("Error", Id.Value);
            }
            return View(funcionario);
        }

        [Authorize(Roles = "Chefe do Setor, Assistente do Setor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarFuncionario(int? Id, [Bind("FuncionarioId,matricula,nome,cargo,setor,email,datanomeacao,dataposse,dataexercicio")] Funcionario funcionario)
        {
            if(Id != funcionario.FuncionarioId)
            {
                _logger.LogError($"Tentando editar funcionario, mas {Id} não corresponde ao id do funcionario");
                return View("Error", Id.Value);
            }
            if(ModelState.IsValid)
            {
                try
                {
                    _appGestaoContext.Update(funcionario);
                    await _appGestaoContext.SaveChangesAsync();
                    _logger.LogInformation($"O funcionario {funcionario.nome} recebeu atualização nos dados cadastrados");
                    ViewBag.UserMessage = "Usuário Atualizado com Sucesso!";
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!FuncionarioExists(funcionario.FuncionarioId))
                    {
                        _logger.LogError($"Tentando alterar um funcionario que não existe: {funcionario.FuncionarioId}");
                        return View("Error", Id);
                    } else
                    {
                        throw;
                    }
                    

                }
                return RedirectToAction(nameof(ListarFuncionarios));
            }
            return View(funcionario);
        }

        [Authorize(Roles = "Chefe do Setor, Assistente do Setor")]
        [HttpGet]
        public async Task<IActionResult> DeletarFuncionario(int? id)
        {
            if(id == null)
            {
                _logger.LogError($"Tentando deletar funcionario, mas id do funcionario é nulo");
                return View("Error", id.Value);
            }
            var funcionario = await _appGestaoContext.Funcionarios.FirstOrDefaultAsync(f => f.FuncionarioId == id);

            if(funcionario == null)
            {
                _logger.LogError($"Tentando deletar funcionario, mas funcionario de id {id} não existe");
                return View("Error", id.Value);
            }
            return View(funcionario);

        }
        [Authorize(Roles = "Chefe do Setor, Assistente do Setor")]
        [HttpPost, ActionName("DeletarFuncionario")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletarFuncionarioConfirmar(int? funcionarioId)
        {
            var funcionario = await _appGestaoContext.Funcionarios.FindAsync(funcionarioId);
            if(funcionario == null)
            {
                return RedirectToAction(nameof(ListarFuncionarios));
            }
            try
            {
                _appGestaoContext.Funcionarios.Remove(funcionario);
                await _appGestaoContext.SaveChangesAsync();
                _logger.LogInformation($"Funcionario de ID {funcionarioId} deletado");
                return RedirectToAction(nameof(ListarFuncionarios));

            } 
            catch(DbUpdateException ex)
            {
                _logger.LogError(ex.ToString());
                return View("Error", funcionarioId.Value);
            }

            
            
            
            
        }

        [Authorize(Roles = "Chefe do Setor, Assistente do Setor")]
        [HttpGet]
        public IActionResult Sugestoes()
        {
            return PartialView();
        }

        //[HttpPost]
        //public IActionResult Sugestoes(string mensagem)
        //{
        //    if (!string.IsNullOrWhiteSpace(mensagem))
        //    {
        //        TempData["Status"] = "Sua mensagem foi submetida";
        //    }
        //    else
        //    {
        //        TempData["Status"] = "Sua mensagem não foi submetida";
        //    }

        //    return RedirectToAction("Index");
        //}

        private bool FuncionarioExists(int id)
        { 
            return _appGestaoContext.Funcionarios.Any(e => e.FuncionarioId == id);
        }

    }
}