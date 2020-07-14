using System.ComponentModel.DataAnnotations.Schema;

namespace gestao.Data.Entities
{
    [Table("FichaDetalhes")]
    public class FichaDetalhe
    {
        public int FichaDetalheId { get; set; }

        public int FichaId { get; set; }
        public int FuncionarioId { get; set; }

        public virtual Funcionario Funcionario { get; set; }
        
        public virtual Ficha Ficha {get; set;}
    }
}