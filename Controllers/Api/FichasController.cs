//using System;
//using System.Collections.Generic;
//using System.Linq;
//using AutoMapper;
//using gestao.Data;
//using gestao.Data.Entities;
//using gestao.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;

//namespace gestao.Controllers
//{
//    [Route("api/funcionarios/{funcionarioid}/fichas")]
//    public class FichasController: Controller
//    {
//        private readonly IRepository _repository;
//        private readonly ILogger<FichasController> _logger;
//        private readonly IMapper _mapper;

//        public FichasController(IRepository repository,
//         ILogger<FichasController> logger, IMapper mapper)
//        {
//            this._repository = repository;
//            this._logger = logger;
//            this._mapper = mapper;
//        }

//        [HttpGet]
//        public IActionResult Get(int funcionarioid)
//        {
//            var funcionario = _repository.GetFuncionarioPorId(funcionarioid);
//            if(funcionario != null ) return Ok(_mapper.Map<IEnumerable<FichaFuncional>, IEnumerable<FichaFuncionalViewModel>>(funcionario.Fichas));
//            return NotFound();
//        }
//        [HttpGet("{id}")]
//        public IActionResult Get(int funcionarioid, int id)
//        {
//            var funcionario = _repository.GetFuncionarioPorId(funcionarioid);
//            if(funcionario != null)
//            {
//                var ficha = funcionario.Fichas.Where(fi => fi.fichafuncId == id).FirstOrDefault();
//                if(ficha != null)
//                {
//                    return Ok(_mapper.Map<FichaFuncional, FichaFuncionalViewModel>(ficha));
//                }
//            }
//            return NotFound();
//        }

//         [HttpPost]
//        public IActionResult Post(int funcionarioid, [FromBody]FichaFuncionalViewModel model)
//        {
//            try
//            {
//                if(ModelState.IsValid)
//                {
                    
//                var novaFicha = _mapper.Map<FichaFuncionalViewModel, FichaFuncional>(model); 
//                _repository.AdicionarFichaParaFunc(funcionarioid,novaFicha);
//                _repository.Commit();

//                var fichaCriadaParaRetornar = _mapper.Map<FichaFuncional,FichaFuncionalViewModel>(novaFicha);
    
                
//                return Created($"/api/funcionarios/{funcionarioid}/fichas/{fichaCriadaParaRetornar.fichaid}", fichaCriadaParaRetornar);
//                } 
//                else
//                {
//                    return BadRequest(ModelState);
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"Falha ao tentar salvar no banco: {ex}");
//            }

//            return BadRequest("Falha ao salva ficha!");
//        }

//    }
//}