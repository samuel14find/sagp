using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using gestao.Data;
using gestao.Data.Entities;
using gestao.ViewModels;
using gestao.Views.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace gestao.Controllers
{
    [Authorize(Roles = "Chefe do Setor, Assistente do Setor")]
    public class TarefasController : Controller
    {
        private readonly AppGestaoContext _context;
        

        public TarefasController(AppGestaoContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> VisualizarTodas(string sortOrder,
                                                        string searchString,
                                                        string currentFilter,
                                                        int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TituloSortParm"] = String.IsNullOrEmpty(sortOrder) ? "titulo_desc" : "";
            ViewData["FuncSortParm"] = String.IsNullOrEmpty(sortOrder) ? "func_desc" : "";
            ViewData["DataCriacaoSortParm"] = sortOrder == "DataCriacao" ? "dataCriacao_desc" : "DataCriacao";
            ViewData["DataPrazoSortParm"] = sortOrder == "DataPrazo" ? "dataPrazo_desc" : "DataPrazo";
          

            //var tarefas = _context.Tarefas.OrderByDescending(x => x.DataCriacao).ToList();
            if(searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            var tarefas = from t in _context.Tarefas
                           select t;
            if (!String.IsNullOrEmpty(searchString))
            {
                tarefas = tarefas.Where(t => t.Descricao.Contains(searchString)
                                        || t.Funcionario.Contains(searchString)
                                        || t.Titulo.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "titulo_desc":
                    tarefas = tarefas.OrderByDescending(t => t.Titulo);
                    break;
                case "func_desc":
                    tarefas = tarefas.OrderByDescending(t => t.Funcionario);
                    break;
                case "DataCriacao":
                    tarefas = tarefas.OrderBy(t => t.DataCriacao);
                    break;
                case "dataCriacao_desc":
                    tarefas = tarefas.OrderByDescending(t => t.DataCriacao);
                    break;
                case "DataPrazo":
                    tarefas = tarefas.OrderBy(t => t.DataLimite);
                    break;
                case "dataPrazo_desc":
                    tarefas = tarefas.OrderByDescending(t => t.DataLimite);
                    break;
                default:
                    tarefas = tarefas.OrderBy(t => t.DataCriacao);
                    break;
            }
            int pageSize = 5;
            //return View(await tarefas.AsNoTracking().ToListAsync());
            return View(await PaginatedList<Tarefa>.CreateAsync(tarefas.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        [HttpGet]
        public IActionResult CriarEditar(int Id = 0)
        {
            ViewBag.Categorias = _context.Categorias.Select(
                x => new SelectListItem() { Text = x.Nome, Value = x.Id.ToString() }).ToList();

            if( Id != 0)
            {
                var tarefas = _context.Tarefas.FirstOrDefault(x => x.Id == Id);
                //var tarefaVm = _mapper.Map<Tarefa,TarefaViewModel>(tarefas);

                return View(tarefas);
            }
            return View();
            
         
        }

        [HttpPost]
        public IActionResult CriarEditar(Tarefa tarefa)
        {
            if(ModelState.IsValid)
            {
                var editarTarefa = _context.Tarefas.FirstOrDefault(x => x.Id == tarefa.Id);
                if(editarTarefa != null)
                {
                    // Atualizar Tarefa
                    editarTarefa.Titulo = tarefa.Titulo;
                    editarTarefa.Descricao = tarefa.Descricao;
                    editarTarefa.CategoriaId = tarefa.CategoriaId;
                    editarTarefa.Funcionario= tarefa.Funcionario;
                    editarTarefa.DataLimite = tarefa.DataLimite;
                    //editarTarefa.MensagemAssociadaId = tarefa.MensagemAssociadaId;
                    editarTarefa.Feita = tarefa.Feita;
                    editarTarefa.Notas = tarefa.Notas;
                    _context.Entry(editarTarefa).State = EntityState.Modified;
                }
                else
                {
                    // Nova Tarefa
                    //var novaFicha = _mapper.Map<FichaFuncionalViewModel, FichaFuncional>(model);
                    //var novaTarefa = _mapper.Map<TarefaViewModel, Tarefa>(tarefa);
                    tarefa.DataCriacao = DateTimeOffset.Now;
                    _context.Tarefas.Add(tarefa);
                }
                _context.SaveChanges();
                return RedirectToAction("VisualizarTodas");
            }

            ViewBag.Categorias = _context.Categorias.Select(
                x => new SelectListItem() { Text = x.Nome, Value = x.Id.ToString() }).ToList();
            return View(tarefa);
        }
    }
}
