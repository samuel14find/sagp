using System.ComponentModel.DataAnnotations.Schema;

namespace gestao.Data.Entities
{
    [Table("FichaDetalhes")]
    public class FichaDetalhe
    {
        public int FichaDetalheId { get; set; }
        public int fichafuncId { get; set; }
        public int FuncionarioId { get; set; }

        [ForeignKey("FuncionarioId")]
        public Funcionario Funcionario { get; set; }

        [ForeignKey("fichafuncId")]
        public FichaFuncional FichaFuncional{get; set;}
    }
}