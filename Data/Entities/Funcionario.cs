using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace gestao.Data.Entities
{
    [Table("Funcionarios")]
    public class Funcionario 
    {
        [BindNever]
        [Key]
        public int FuncionarioId { get; set; }

        [Required(ErrorMessage = "Favor entrar com a matricula")]
        [StringLength(10, MinimumLength = 5)]
        public string matricula { get; set; }

        [Required(ErrorMessage = "Favor entrar com a nome do funcionário")]
        [StringLength(100)]
        public string nome { get; set; }

        [Required(ErrorMessage = "Favor entrar com o cargo do funcionário")]
        [StringLength(50)]
        public string cargo { get; set; }

        [Required(ErrorMessage = "Favor entrar com o setor do funcionário")]
        [StringLength(50)]
        public string setor { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "Entre com um email válido")]
        public string email { get; set; }

        
        [DataType(DataType.DateTime)]
        public DateTimeOffset? datanomeacao { get; set; }

       
        [DataType(DataType.DateTime)]
        public DateTimeOffset? dataposse { get; set; }

        
        [DataType(DataType.DateTime)]
        public DateTimeOffset? dataexercicio { get; set; }


        public ICollection<FichaFuncional> Fichas {get; set;} = new List<FichaFuncional>(); // Propriedade navegacional de coleção
    }
    
}