using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using gestao.Data.Entities;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;

namespace gestao.Data
{
    //  Comentários
    // Observe que aqui vou implementar um serviço para fazer Loog de Erros
    // Observe como usei o ILogger<GestaoRepository>, de modo que fiz uma ligação
    // a esse tipo, GestaoRepository, assim quando houver emissão de dados, podemos 
    // ver de onde eles vieram. 

    public class GestaoRepository : IRepository
    {
        private readonly AppGestaoContext _context;
        private readonly ILogger<GestaoRepository> _logger;

       

        public GestaoRepository(AppGestaoContext context, ILogger<GestaoRepository> logger)
        {
            this._logger = logger;
            this._context = context;

        }
        public Funcionario Add(Funcionario novoFuncionario)
        {
            try
            {
            _logger.LogInformation($"Funcionario {novoFuncionario.nome} adicionado á base!");
            _context.Add(novoFuncionario);
            return novoFuncionario;

            } catch(Exception ex)
            {
                _logger.LogError($"Falha em adicionar funcionario. {ex}");
                return null;
            }
        }
        public IEnumerable<Funcionario> GetFuncionarios()
        {
            try
            {
                
             _logger.LogInformation("Bucando todos os funcionarios...");
             return _context.Funcionarios.OrderBy(f => f.nome).ToList();
                
            } catch (Exception ex)
            {
                _logger.LogError($"Falha em obter todos os funcionarios: {ex}");
                return null;
            }
        }
        public int Commit()
        {
            return _context.SaveChanges();
        }

        public Funcionario GetFuncionarioPorMatricula(string matricula)
        {
            return _context.Funcionarios.Where(f => f.matricula == matricula).FirstOrDefault();
        }
         public Funcionario GetFuncionarioPorNome(string nome)
        {
            return _context.Funcionarios.Where(f => f.nome == nome).FirstOrDefault();
        }

        public IEnumerable<Ficha> GetFichas()
        {
            //return _context.Fichas.Include(f => f.funcionario).ToList();
            return null;
        }

        public Funcionario GetFuncionarioPorId(int idFunc)
        {
            try
            {
            _logger.LogInformation($"Buscando o funcionário de id: {idFunc}");
            return _context.Funcionarios
            //.Include(fi => fi.Fichas)
            .Where(func => func.FuncionarioId == idFunc)
            .FirstOrDefault();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Erro ao buscar um funcionário pela id: {ex}");
                return null;
            }
        }

        public Ficha GetFichaPorId(int id)
        {
            //return _context.Fichas
            //.Include(f => f.funcionario)
            //.Where(f => f.fichafuncId == id).FirstOrDefault();
            return null;
        }

        //  Comentario:
        // Ver comentários que adicionar na interface 
        public void AdicionarEntidade(object model)
        {
           _context.Add(model);
        }

        public void AdicionarFichaParaFunc(int funcId, Ficha novaFicha)
        {
            try 
            {
              
                var funcionario = GetFuncionario(funcId, false);
                _logger.LogInformation($"Adicionado ficha à base..");
                //funcionario.Fichas.Add(novaFicha); 
            } catch (Exception ex)
            {
                _logger.LogError($"Falha ao adicionar a ficha: {ex}");
            }      
            
        }

        public Funcionario GetFuncionario(int funcId, bool includeFicha)
        {
            try
            {

            if(includeFicha)
            {
                _logger.LogInformation($"Retornado funcionario para adicionar ficha");
                return _context.Funcionarios
                    //.Include(f => f.Fichas)
                    .Where(c => c.FuncionarioId==funcId)
                    .FirstOrDefault();

            }
            return _context.Funcionarios.Where(f => f.FuncionarioId == funcId).FirstOrDefault();
                } 
            catch(Exception ex)
            {
                _logger.LogError($"Erro ao buscar funcionario para adicionar ficha:{ex}");
                return null;
            }
           
        }

        // public IEnumerable<CarreiraAdministrativa> GetCarreiraAdm => _context.CarreiraAdministrativas;

        // public IEnumerable<CarreiraProfessor> GetCarreiraProf => _context.CarreiraProfessores;

        public IEnumerable<Funcionario> GetFuncionarioAdm()
        {
             try
            {
                   
             _logger.LogInformation("Bucando todos os funcionarios Admin");
             return _context.Funcionarios
             //.Include(fi => fi.Fichas)
             .OrderBy(f => f.nome)
             .ToList();
               
                
            } catch (Exception ex)
            {
                _logger.LogError($"Falha em obter todos os funcionarios Admin: {ex}");
                return null;
            }
        }

        public IEnumerable<Funcionario> GetFuncionarioProf()
        {
             try
            {
                   
             _logger.LogInformation("Bucando todos os funcionarios Profe");
             return _context.Funcionarios
             //.Include(fi => fi.Fichas)
             .OrderBy(f => f.nome)
             .ToList();
               
                
            } catch (Exception ex)
            {
                _logger.LogError($"Falha em obter todos os funcionarios Professor: {ex}");
                return null;
            }

        }

        public DateTimeOffset GetDataExercicio(int funcionarioId)
        {
            var data = from func in _context.Funcionarios
                       where func.FuncionarioId == funcionarioId
                       select func.dataexercicio;
            DateTimeOffset value = (DateTimeOffset)data.FirstOrDefault();
                return value;
        }

        public IEnumerable<object> FuncionariosPorSetor()
        {
            var funcionariosPorSetor = from func in _context.Funcionarios
                                       group func by func.setor into funcGrupo
                                       select new
                                       {
                                           Setor = funcGrupo.Key,
                                           Count = funcGrupo.Count(),
                                       };
            return funcionariosPorSetor;
        }

        public IEnumerable<object> TarefaPorFuncionario()
        {
            var tarefasPorFunc = from tar in _context.Tarefas
                                 group tar by tar.Funcionario into tarGrupo
                                 select new
                                 {
                                     funcionario = tarGrupo.Key,
                                     count = tarGrupo.Count(),
                                 };
            return tarefasPorFunc;
        }

        public IEnumerable<object> FuncionariosPorCarreira()
        {


            var carreiraPorFunc = (from carr in _context.FuncionariosCarreira
                                  select new
                                  {

                                      carreira = carr.CarreiraIso5,
                                      funcionario = carr.NomeFuncionario
                                  }).Distinct();

                                  
            var carreiraPorFunc_2 = from ca in carreiraPorFunc
                                  group ca by ca.carreira into carrGrupo
                                  select new
                                  {
                                     carreira = carrGrupo.Key,
                                     count = carrGrupo.Count()
                                  };



            return carreiraPorFunc_2;

        }
        public IEnumerable<Object> TitulosFicha()
        {
            var q_1 = from f in _context.Funcionarios                    
                      select new 
                      {
                          funcList = (from fufi in _context.FichaDetalhes
                                      join fu in _context.Funcionarios on fufi.FuncionarioId
                                      equals fu.FuncionarioId where fufi.FuncionarioId == f.FuncionarioId 
                                      orderby fu.nome
                                      select fu.nome).First(),


                          fichaList = (from fifu in _context.FichaDetalhes
                                       join fi in _context.Fichas on fifu.FichaId equals fi.FichaId
                                       where fifu.FuncionarioId == f.FuncionarioId 
                                       select fi.descricao).ToList()        
                                      
                      };

            var q_2 = from f in _context.FichaDetalhes
                      select new
                      {
                          funcList = (from fufi in _context.Funcionarios
                                      join fu in _context.FichaDetalhes on fufi.FuncionarioId
                                      equals fu.FuncionarioId
                                      where fufi.FuncionarioId == f.FuncionarioId
                                      select fu.Funcionario.nome).FirstOrDefault(),


                          fichaList = (from fifu in _context.Fichas
                                       join fi in _context.FichaDetalhes on fifu.FichaId equals fi.FichaId
                                       where fifu.FichaId == f.FichaId
                                       select fi.Ficha.descricao).ToList()

                      };




            //var q_2 = from f in _context.FichaDetalhes
            //          join fi in _context.Funcionarios on f.FuncionarioId equals fi.FuncionarioId
            //          join fich in _context.Fichas on f.FichaId equals fich.FichaId into gj
            //          select new { Nome = f.Funcionario.nome, Fichas = gj };

            //var q_2 = from f in _context.FichaDetalhes
            //          join fi in _context.Fichas on f.FichaId equals fi.FichaId into gj
            //          join fu in _context.Funcionarios on f.FuncionarioId equals fu.FuncionarioId
            //          where
            //          select new
            //          {
            //              funcionario = f.Funcionario.nome,
            //              fichas = gj
            //          };

            //var q_2 = from f in _context.FichaDetalhes
            //          join ficha in _context.Fichas on f.FichaId equals ficha.FichaId into gj
            //          select new { Nome = f.Funcionario.nome, Fichas = gj };

            //var display = from ti in q_1
            //             group ti by ti.fichaList into tiGroup
            //             select new
            //             {
            //                 fichaList = tiGroup.Key,
            //                 count = tiGroup.Count()
            //             };

            return q_2;
        }

        public IEnumerable<Object> CategoriaTarefas()
        {
            var query = from tarefa in _context.Tarefas
                        join categ in _context.Categorias on tarefa.CategoriaId equals categ.Id 
                        select new
                        {
                            Tarefa =  tarefa.Titulo,
                            Categoria = categ.Nome
                        };

            var display = from tar in query
                          group tar by tar.Categoria into categoriaGroup
                          select new
                          {
                              categoria = categoriaGroup.Key,
                              count = categoriaGroup.Count()
                          };

            return display;
        }
    }
}