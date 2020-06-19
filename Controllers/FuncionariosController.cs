using System;
using System.Collections.Generic;
using gestao.Data;
using gestao.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace gestao.Controllers
{
    //  Comentarios:
    // Essa classe será um controler para "expor" um API endpoint. Lembrando que no 
    // ASP.NET Core não tem mais uma biblioteca expecífica para APIs. Agora fica tudo
    // na biblioteca Controller ou ControllerBase. 
    // O atributo [ApiController] ajuda na execução de ferramentas de documentação de API
    // deixando elas saberem que o controller é uma api. 
    // E também o atributo [Produces] também vai ajudar nessas ferramentas, dizendo que esse 
    // API controller vai sempre retornar um Json. 
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    public class FuncionariosController: Controller
    {
        private readonly IRepository _repository;
        private readonly ILogger<FuncionariosController> _logger;

        public FuncionariosController(IRepository repository, ILogger<FuncionariosController> logger)
        {
            this._repository = repository;
            this._logger = logger;
        }

        //  Comentários:
        // Aqui vou permitir a negociação do conteúdo. O truque é retornar um ActionResult.
        // Por padrão o MVC 6 retorna JSON. Mas posso mudar isso a adicionar outras formatos de retorno.
        // Retornando um OK(), ou dizendo que o Status Code é o 200. O Ok() está ligado ao 200
        // e o BadRequest está ligado ao 400.
        // Aqui temos a questão de documentar a api, utilizando ferramentas como Swagger. Por 
        // isso estou colocando o type ActionResult e não IActionResult. O IActionResult esconde
        // o tipo de dado real que está sendo retornado. Assim estamos definindo o tipo de retorno 
        // que é um IEnumerable do tipo Funcionario. A ideia é deixar o código mais "limpo" ou consiso 
        // que permite executar ferramentas para ajudar a documentar a API. 
        // ATENÇÃO. Os atributos [ProducesResponseType] necessitaram que eu fosse lá na 
        // classe Startup e adicionasse um SetCompatibilityVersion.
        // Com esses atributos estou dizendo: "Dessa api eu espero esses dois tipos de respnse"
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Funcionario>> Get()
        {
            try
            {

            return Ok(_repository.GetFuncionarios());
            } catch(Exception ex)
            {
                _logger.LogError($"Falha em obter os funcionarios: {ex}");
                return BadRequest("Falha em obter os funcionarios");
            }
        }
    }
}