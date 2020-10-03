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
    public class FichaFuncionarioController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly ILogger<FichaFuncionarioController> _logger;

        public FichaFuncionarioController(IRepository repository, ILogger<FichaFuncionarioController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var fichasPorFunc = _repository.TitulosFicha();
                _logger.LogInformation($"Chamou a API FichaFuncionario com sucesso");
                return Ok(await Task.FromResult(fichasPorFunc.ToList()));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Falha ao bucas as fichas por funcionario {ex}");

                return BadRequest("Falha ao bucas as fichas por funcionario");

            }

        }
    }
}
