using gestao.Data;
using gestao.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace gestao.Controllers
{

    [Authorize(Roles = "Chefe do Setor, Assistente do Setor")]
    public class FichaController : Controller
    {
        private readonly IFichaFuncionalRepository _fichaRepository;
        private readonly CarrinhoFicha _carrinho;
        private readonly ILoggerFactory _loggerFactory;

        public FichaController(IFichaFuncionalRepository fichaRepository, CarrinhoFicha carrinho,
            ILoggerFactory loggerFactory)
        {
            _fichaRepository = fichaRepository;
            _carrinho = carrinho;
            _loggerFactory = loggerFactory;
        }
        [HttpGet]
        public IActionResult PreencherFicha()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PreencherFicha(Ficha ficha)
        {
            var funcionarios = _carrinho.GetCarrinhoFichaItens();
            _carrinho.CarrinhoFichaItens = funcionarios;

            if(_carrinho.CarrinhoFichaItens.Count == 0)
            {
                ModelState.AddModelError("", "Ninguém para adicionar ficha...");
            }
            if(ModelState.IsValid)
            {
                _fichaRepository.CriarFicha(ficha);
                _carrinho.LimparCarrinho();
                return RedirectToAction("FichaPreenchida");
            }
            return View(ficha);
        }

        public IActionResult FichaPreenchida()
        {
            ViewBag.FichaPreenchidaMensagem = "Ficha incluída com sucesso";
            return View();
        }
    }
}
