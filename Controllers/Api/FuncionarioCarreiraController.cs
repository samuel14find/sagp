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
    public class FuncionarioCarreira : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly ILogger<FuncionarioCarreira> _logger;

        public FuncionarioCarreira(IRepository repository, ILogger<FuncionarioCarreira> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var carreiraPorFunc = _repository.FuncionariosPorCarreira();
                _logger.LogInformation($"Chamou a API FuncionarioCarreira com sucesso");
                return Ok(await Task.FromResult(carreiraPorFunc.ToList()));

            }
            catch(Exception ex)
            {
                _logger.LogError($"Falha contar os funcionarios por carreira {ex}");
                
                return BadRequest("Falha ao contar os funcionarios por carreira");

            }

        }
    }
}
