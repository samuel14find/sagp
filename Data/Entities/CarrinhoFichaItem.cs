using System.ComponentModel.DataAnnotations;

namespace gestao.Data.Entities
{
    // Essa classe vai representar um funcionario selecionada para incluir no carrinho 
    // de fichas.
    public class CarrinhoFichaItem
    {
        public int CarrinhoFichaItemId { get; set; }
        public Funcionario Funcionario {get; set;}

        // Com esse campo vou conseguir fazer um relacionamento entre a Entidade CarrinhoFichaItem
        // e a entidade CarrinhoFicha. Aqui vou usar o recurso chamado GUI
        [StringLength(200)]
        public string CarrinhoFichaId { get; set; } 


    }
}