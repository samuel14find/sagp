using gestao.Service;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace gestao.Data.Entities
{
    public class Tarefa : IValidatableObject
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe um Título para Tarefa")]
        [Display(Name = "Nome da Tarefa")]
        public string Titulo { get; set; }

        [Display(Name = "Descrição da Tarefa")]
        [Required(ErrorMessage = "Informe um Descrição da Tarefa")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe a data limite para fazer a tarefa")]
        [Display(Name = "Data/Hora Limite para Fazer a Tarefa no no formato dd/mm/aaaa hh:mm     ")]
        [DataType(DataType.DateTime)]
        [ValidaDataTarefa(ErrorMessage = "Data incorreta. Colocar data no futuro")]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTimeOffset DataLimite { get; set; }


        public DateTimeOffset? DataCriacao { get; set; }

        [StringLength(1000, MinimumLength = 20)]
        public string Notas { get; set; }
        public bool Feita { get; set; }

        [Display(Name = "Quem vai Fazer")]
        [Required(ErrorMessage = "Informe quem vai fazer a Tarefa")]
        public string Funcionario { get; set; }

        //public int MensagemAssociadaId { get; set; }
        [Display(Name = "Categoria da Tarefa")]
        [Required(ErrorMessage = "Informe em qual categoria se encaixa a tarefa")]
        public int CategoriaId { get; set; }

        
        //public virtual Mensagem MensagemAssociada { get; set; }
        public CategoriaTarefa CategoriaTarefa { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            
            var erros = new List<ValidationResult>();
            if(Feita && string.IsNullOrWhiteSpace(Notas))
            {
                erros.Add(new ValidationResult("Se você completou sua tarefa, deve-se adicionar alguma nota"));
           }

            return erros;
        }
    }
}
