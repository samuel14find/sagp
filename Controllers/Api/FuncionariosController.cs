using System;
using System.Collections.Generic;
using System.Threading;
using AutoMapper;
using gestao.Data;
using gestao.Data.Entities;
using gestao.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace gestao.Controllers
{
    [Route("api/[Controller]")]
    public class FuncionariosController: Controller
    {
        private readonly IRepository _repository;
        private readonly ILogger<FuncionariosController> _logger;
        private readonly IMapper _mapper;

        public FuncionariosController(IRepository repository, ILogger<FuncionariosController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

                 [HttpGet]
                public IActionResult Get( )
               {
                    try
                    {
                var results = _repository.GetFuncionarios();
                // Comentarios:
                // Ao usar o Mapper, se for mapear collections, a dica é mapear 
                // de collection para collection, de list para list etc. 
                _logger.LogInformation($"Chamou com sucesso a api Funcionarios");
                        return Ok(_mapper.Map<IEnumerable<Funcionario>, IEnumerable<FuncionarioViewModel>>(results));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation($"Verificar log de error. Erro ao tentar recuperar dos os funcionarios: {ex}");
                        _logger.LogError($"Falha ao buscar os funcionarios: {ex}");
                        return BadRequest("Falha ao buscar os funcionarios");
                    }
                }
    }
}