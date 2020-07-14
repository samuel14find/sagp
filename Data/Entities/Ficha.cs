using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace gestao.Data.Entities
{
    // FuchaFuncional vai ser uma entidade dependente no relacionamento com Funcionario. 
    //  O relacionamento entre Funcionario e FichaFuncional é 1 - n. 
    // Vamos definir a propriedade de navegação aqui. 
    [Table("Fichas")]
   
    public class Ficha
    {
        public int FichaId { get; set; }
        public List<FichaDetalhe> FichaItens {get; set;}

        [Required(ErrorMessage="Informe um título para ficha")]
        [Display(Name = "Título da Ficha")]
        [StringLength(10)]
        public string titulo { get; set; } 

        [Required(ErrorMessage="Informa a descrição da ficha")]  
        [Display(Name="Descrição da Ficha")]
        public string descricao { get; set; }

        [Required(ErrorMessage="Informa a data da ficha")]
        [DataType(DataType.DateTime)]
       public DateTimeOffset dataficha { get; set; }

       //public Funcionario funcionario {get; set;} // Propriedade de navegação de referência

    }
}