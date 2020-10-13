using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestao.Data.Entities
{
    public class FuncionarioCarreira
    {
        [Key]
        public int IdentificadorFuncionarioCarreira {get; set;}
        [Required]
        [MaxLength(128)]
        public string NomeFuncionario {get; set;}

         [MaxLength(20)]
        public string CarreiraIso5 { get; set; }

        [ForeignKey("CarreiraIso5")]
       public Carreira carreira {get; set;} 

       [MaxLength(10)]
        public string ProgressaoCode { get; set; }

        [ForeignKey("ProgressaoCode")]
       public ProgressaoCarreira progressao {get; set;} 
    }
}