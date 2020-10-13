using System;
using System.ComponentModel.DataAnnotations;
using gestao.Data.Entities;

namespace gestao.ViewModels
{
    public class FuncionarioCarreiraDisplayViewModel
    {
        [Display(Name="Identificador da Carreira do Funcionario")]
        public int IdentificadorFuncionarioCarreira { get; set; }
        
        [Display(Name="Nome do Funcionario")]
        public string NomeFuncionario { get; set; }

        [Display(Name="Carreira")]
        public string NomeCarreira { get; set; }
        [Display(Name = "Progressao do Funcionario")]
        public string NomeProgressao { get; set; }
        
    }
}