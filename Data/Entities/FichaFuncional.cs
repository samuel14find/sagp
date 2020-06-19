using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestao.Data.Entities
{
    // FuchaFuncional vai ser uma entidade dependente no relacionamento com Funcionario. 
    //  O relacionamento entre Funcionario e FichaFuncional é 1 - n. 
    // Vamos definir a propriedade de navegação aqui. 
    [Table("Fichas")]
    public class FichaFuncional
    {
        [Key]
        public int fichafuncId { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
       public DateTimeOffset? dataficha { get; set; }

       [ForeignKey("FuncionarioId")]
       public Funcionario funcionario {get; set;} // Propriedade de navegação de referência

    }
}