using System;
using gestao.Data.Entities;
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
        public int Commit()
        {
            return _context.SaveChanges();
        }

    }
}