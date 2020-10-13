using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using gestao.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace gestao.ViewModels
{
    public class FuncionarioEditViewModel
    {
        [Display(Name="Identificador da Carreira do Funcionario")]
        public int IdentificadorFuncionarioCarreira { get; set; }

        [Required]
        [Display(Name="Nome do Funcionario")]
        [StringLength(75)]
        public string NomeFuncionario { get; set; }
        [Required]
        [Display(Name="Carreira")]
        public string SelectedCarreiraIso5 { get; set; }
        public IEnumerable<SelectListItem> Carreiras { get; set; }

        [Required]
        [Display(Name="Seleciona a Progressão")]
        public string SelectedProgressaoCode { get; set; }
        public IEnumerable<SelectListItem> Progressoes {get; set;}
        
    }
}