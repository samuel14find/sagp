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
    public class SetoresController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly ILogger<SetoresController> _logger;

        public SetoresController(IRepository repository, ILogger<SetoresController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var funcPorSetor = _repository.FuncionariosPorSetor();
                _logger.LogInformation($"Chamou a API Setor com sucesso");
                return Ok(await Task.FromResult(funcPorSetor.ToList()));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Falha contar os setores {ex}");

                return BadRequest("Falha ao buscar os setores");

            }

        }
    }
}
