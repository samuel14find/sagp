using System;
using System.Collections.Generic;
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
        private readonly ILogger<FichasController> _logger;
        private readonly IMapper _mapper;

        public FuncionariosController(IRepository repository, ILogger<FichasController> logger, IMapper mapper)
        {
            this._repository = repository;
            this._logger = logger;
            this._mapper = mapper;
        }

         [HttpGet]
        public IActionResult Get()
        {
            try
            {
                // Comentarios:
                // Ao usar o Mapper, se for mapear collections, a dica é mapear 
                // de collection para collection, de list para list etc. 
                return Ok(_mapper.Map<IEnumerable<Funcionario>, IEnumerable<FuncionarioViewModel>>(_repository.GetFuncionarios()));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Falha ao buscar os funcionarios: {ex}");
                return BadRequest("Falha ao buscar os funcionarios");
            }
        }

         [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
             try
            {
                var funcionario = _repository.GetFuncionarioPorId(id);
                //  Comentário:
                // Isso aqui funciona assim: Pega a ficha que estão passando e retorna uma versão 
                // mapeada ao FichaFuncionalViewModel visto que sempre vamos querer retornar uma view model
                if(funcionario != null) return Ok(_mapper.Map<Funcionario, FuncionarioViewModel>(funcionario));
                else return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Falha ao buscar o funcionario: {ex}");
                return BadRequest("Falha ao buscar o funcionario");
            }
        }

        //  Comentarios
        // A minha intenção aqui é estruturar o "body" da requisição com os dados 
        // que quero salvar. Por isso usei o atributo [FromBody].
        // Observe que se consegui adicionar a entidade com sucesso, vou retornar
        // Created(). Assim estamos em sintonia com o jeito de trabalhar do Http
        // Com Post, se estamos criando um objeto, o correto é retornar esse Created.
        // O status de created é 201. 
        // Observe aqui que justamente por estar usando o ViewModel vou usar o ModelState 
        // para validar as coisas. 
        [HttpPost]
        public IActionResult Post([FromBody]FuncionarioViewModel model)
        {
        
            try
            {
                if(ModelState.IsValid)
                {
                    var novoFuncionario = _mapper.Map<FuncionarioViewModel, Funcionario>(model);
                    //  Comentários:
                    // Aqui vou fazer uma pequena validação. Verifica se 
                    // não vou passado data alguma. Isso porque não passei 
                    // required na data lá no FichaFuncionalViewModel.

                    if( novoFuncionario.dataexercicio == DateTimeOffset.MinValue)
                    {
                        novoFuncionario.dataexercicio = DateTimeOffset.UtcNow;
                    };
                _repository.AdicionarEntidade(novoFuncionario);
                _repository.Commit();

                //  Comentários:
                // Como eu convertir o mode para novaFicha, agora faço o trabalho 
                // o contrário. Mas observe que para isso funcionar, ir lá no profile e 
                // colocar ReverseMap.
                return Created($"/api/funcionarios/{novoFuncionario.FuncionarioId}", _mapper.Map<Funcionario,FuncionarioViewModel>(novoFuncionario));
                } else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Falha ao tentar salvar no banco: {ex}");
            }
            return BadRequest("Falha ao salva ficha!");
        }

    }

}
