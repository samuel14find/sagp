using System;
using System.Collections.Generic;
using System.Linq;
using gestao.Data.Entities;
using Microsoft.EntityFrameworkCore;
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
             _logger.LogInformation("Bucando todos os funcionarios");
             return _context.Funcionarios
             .Include(fi => fi.Fichas)
             .OrderBy(f => f.nome)
             .ToList();
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

        public IEnumerable<FichaFuncional> GetFichas()
        {
           return _context.Fichas.Include(f => f.funcionario).ToList();
        }

        public Funcionario GetFuncionarioPorId(int idFunc)
        {
            try
            {
            _logger.LogInformation($"Buscando o funcionário de id: {idFunc}");
            return _context.Funcionarios
            .Include(fi => fi.Fichas)
            .Where(func => func.FuncionarioId == idFunc)
            .FirstOrDefault();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Erro ao buscar um funcionário pela id: {ex}");
                return null;
            }
        }

        public FichaFuncional GetFichaPorId(int id)
        {
           return _context.Fichas
           .Include(f => f.funcionario)
           .Where(f => f.fichafuncId == id).FirstOrDefault();
        }

        //  Comentario:
        // Ver comentários que adicionar na interface 
        public void AdicionarEntidade(object model)
        {
           _context.Add(model);
        }

        public void AdicionarFichaParaFunc(int funcId, FichaFuncional novaFicha)
        {
            try 
            {
              
                var funcionario = GetFuncionario(funcId, false);
                _logger.LogInformation($"Adicionado ficha à base..");
                funcionario.Fichas.Add(novaFicha); 
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
                    .Include(f => f.Fichas)
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
    }
}