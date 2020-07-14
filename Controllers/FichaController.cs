using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gestao.Data;
using gestao.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace gestao.Controllers
{
    public class FichaController : Controller
    {
        private readonly IFichaFuncionalRepository _fichaRepository;
        private readonly CarrinhoFicha _carrinho;

        public FichaController(IFichaFuncionalRepository fichaRepository, CarrinhoFicha carrinho)
        {
            _fichaRepository = fichaRepository;
            _carrinho = carrinho;
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
