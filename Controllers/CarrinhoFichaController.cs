using gestao.Data;
using gestao.Data.Entities;
using gestao.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gestao.Controllers
{

    [Authorize(Roles = "Chefe do Setor, Assistente do Setor")]
    public class CarrinhoFichaController : Controller
    {
        // Index(). Vou definir aqui uma ViewModel, chamar de CarrinhoFichaViewModel
        // AdicionarFuncionarioNoCarrinhoFicha
        // RemoverFuncionarioDoCarrinhoFicha
        // Criar a View Index.cshtml na pasta /View/CarrinhoFicha
        private readonly IRepository _contextFuncionario;
        private readonly CarrinhoFicha _carrinhoFicha;

        public CarrinhoFichaController(IRepository contextFuncionario, CarrinhoFicha carrinhoFicha)
        {
            this._contextFuncionario = contextFuncionario;
            this._carrinhoFicha = carrinhoFicha;
        }

        public IActionResult Index()
        {
            var itens = _carrinhoFicha.GetCarrinhoFichaItens();
            _carrinhoFicha.CarrinhoFichaItens = itens;

            var carrinhoFichaViewModel = new CarrinhoFichaViewModel()
            {
                CarrinhoFicha = _carrinhoFicha

            };

            return View(carrinhoFichaViewModel);
        }

       
        public RedirectToActionResult AdicionarFuncionarioNoCarrinhoFicha(int funcionarioId)
        {
            var funcionarioSelecionado = _contextFuncionario.GetFuncionarioPorId(funcionarioId);
            if(funcionarioSelecionado != null)
            {
                _carrinhoFicha.AdicionarAoCarrinho(funcionarioSelecionado);
            }
            return RedirectToAction("Index");
        }

       
        public IActionResult RemoverFuncionarioDoCarrinhoFicha(int funcionarioId)
        {
            var funcionarioSelecionado = _contextFuncionario.GetFuncionarioPorId(funcionarioId);
            if(funcionarioSelecionado != null)
            {
                _carrinhoFicha.RemoverDoCarrinho(funcionarioSelecionado);
                //  return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


    }
}