using System;
using gestao.Data;
using gestao.Data.Entities;
using gestao.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace gestao.Controllers
{
    [Route("api/[Controller]")]
    public class FichasController: Controller
    {
        private readonly IRepository _repository;
        private readonly ILogger<FichasController> _logger;

        public FichasController(IRepository repository, ILogger<FichasController> logger)
        {
            this._repository = repository;
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetFichas());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Falha ao buscar as fichas: {ex}");
                return BadRequest("Falha ao buscar as fichas");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
             try
            {
                var ficha = _repository.GetFichaPorId(id);
                if(ficha != null) return Ok(ficha);
                else return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Falha ao buscar a ficha: {ex}");
                return BadRequest("Falha ao buscar as ficha");
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
        public IActionResult Post([FromBody]FichaFuncionalViewModel model)
        {
        
            try
            {
                if(ModelState.IsValid)
                {
                    var novaFicha = new FichaFuncional()
                    {
                        fichafuncId = model.fichaid,
                        titulo = model.Titulo,
                        descricao = model.Descricao,
                        dataficha = model.DataFicha
                    };
                    //  Comentários:
                    // Aqui vou fazer uma pequena validação. Verifica se 
                    // não vou passado data alguma. Isso porque não passei 
                    // required na data lá no FichaFuncionalViewModel.

                    if( novaFicha.dataficha == DateTimeOffset.MinValue)
                    {
                        novaFicha.dataficha = DateTimeOffset.UtcNow;
                    };
                _repository.AdicionarEntidade(novaFicha);
                _repository.Commit();
                //  Comentários:
                // Como eu convertir o mode para novaFicha, agora faço o trabalho 
                // o contrário
                var vm = new FichaFuncionalViewModel()
                {
                    fichaid = novaFicha.fichafuncId,
                    Titulo = novaFicha.titulo,
                    Descricao = novaFicha.descricao,
                    DataFicha = novaFicha.dataficha
                };

                //  Comentários:
                // Nesse ponto já tenho o id gerado
                //  pelo banco e se eu precisar acessar
                // outros campos eu passo model de volta. 
                return Created($"/api/fichas/{vm.fichaid}", model);
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