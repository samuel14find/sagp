using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gestao.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace gestao.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaFuncionarioController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly ILogger<TarefaFuncionarioController> _logger;

        public TarefaFuncionarioController(IRepository repository, ILogger<TarefaFuncionarioController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var tarefaPorFunc = _repository.TarefaPorFuncionario();
                _logger.LogInformation($"Chamou a API TarefaFuncionario com sucesso");
                return Ok(await Task.FromResult(tarefaPorFunc.ToList()));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Falha contar as tarefas por funcionario {ex}");
                return BadRequest("Falha ao buscar as tarefas");
            }
        }
    }
}
