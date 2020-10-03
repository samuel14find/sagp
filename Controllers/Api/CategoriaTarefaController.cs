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
    public class CategoriaTarefaController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly ILogger<CategoriaTarefaController> _logger;

        public CategoriaTarefaController(IRepository repository, ILogger<CategoriaTarefaController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var categoriaTarefa = _repository.CategoriaTarefas();
                _logger.LogInformation($"Chamou a API CategoriaTarefa com sucesso");
                return Ok(await Task.FromResult(categoriaTarefa.ToList()));

            }
            catch(Exception ex)
            {
                _logger.LogError($"Falha contar as categorias {ex}");
                
                return BadRequest("Falha ao contar os categorias");

            }

        }
    }
}
