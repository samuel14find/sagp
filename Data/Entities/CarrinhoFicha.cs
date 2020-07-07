using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace gestao.Data.Entities
{
    //Essa classe vai ser trabalhada em memória
    public class CarrinhoFicha
    {
        private readonly AppGestaoContext _context;

        public CarrinhoFicha(AppGestaoContext context)
        {
            this._context = context;
        }
        // Comentarios
        // Aqui vou usar a Struct Guid para gerar um Id em memória
        // para cada CarrinhoFicha. A struct Guid representa um identificador único global.
        public string CarrinhoFichaId { get; set; } // Identifica de forma única meu carrinho. Usar Guid
        public List<CarrinhoFichaItem> CarrinhoFichaItens{get; set;}

        public static CarrinhoFicha GetCarrinho(IServiceProvider services)
        {
            // Aqui define ma sessão, acessando o contexto atual. Uso aqui o operador 
            // de condicional nulla, ou seja, vai retornar uma session, se o operador 
            // IHttpContextAccessor não for nulo 
            ISession session =
                services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            
            // Aqui obtenho um serviço do tipo do nosso contexto
            var context = services.GetService<AppGestaoContext>();

            // Aqui obtenho ou gero o Id do Carrinho de Fichas. Aqui vou usar o operador 
            // de coalescencia nula. Ele vai retornar o CarrinhoId da sessão se o carrinho
            // existir. Se ele não existir, vai dar um NewGuid para gerar o id. 
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            // Aqui vou fazer a atribuição do Id do carrinho à sessão
            session.SetString("CarrinhoId", carrinhoId);

            // Aqui faço o retorno do Carrinho de Fichas com o Id definido. 
            return new CarrinhoFicha(context)
            {
                CarrinhoFichaId = carrinhoId
            };
        }

        public void AdicionarAoCarrinho(Funcionario funcionario)
        {
            var carrinhoFichaItem = 
                _context.CarrinhoFichaItens.SingleOrDefault(
                    c => c.Funcionario.FuncionarioId == funcionario.FuncionarioId && c.CarrinhoFichaId == CarrinhoFichaId);
            
            // Aqui vou verificar se o Carrinho de Fichas existe, se ele não existir 
            // eu crio um
            if(carrinhoFichaItem == null)
            {
                carrinhoFichaItem = new CarrinhoFichaItem
                {
                    CarrinhoFichaId = CarrinhoFichaId,
                    Funcionario = funcionario
                };
                _context.CarrinhoFichaItens.Add(carrinhoFichaItem);
                _context.SaveChanges();
            } 
            else 
            {
                //Implementar alguma lógica para verificar quando o carrinho de ficha 
                // já tem algum funcionario adicionar
            }
        }

        public void RemoverDoCarrinho(Funcionario funcionario)
        {
            var carrinhoFichaItem = 
                _context.CarrinhoFichaItens.SingleOrDefault(
                    c => c.Funcionario.FuncionarioId == funcionario.FuncionarioId && c.CarrinhoFichaId == CarrinhoFichaId);
            
            _context.CarrinhoFichaItens.Remove(carrinhoFichaItem);
            _context.SaveChanges();

        }

        public List<CarrinhoFichaItem> GetCarrinhoFichaItens()
        {
            return CarrinhoFichaItens ??
                (CarrinhoFichaItens = _context.CarrinhoFichaItens.Where(c => c.CarrinhoFichaId == CarrinhoFichaId).Include(f => f.Funcionario).ToList());
        }

        public void LimparCarrinho()
        {
            var carrinhoItens = _context.CarrinhoFichaItens
                                .Where(carrinho => carrinho.CarrinhoFichaId == CarrinhoFichaId);
            _context.CarrinhoFichaItens.RemoveRange(carrinhoItens);
            _context.SaveChanges();
            
        }


    }  
}
