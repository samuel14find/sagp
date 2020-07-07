using System;
using System.ComponentModel.DataAnnotations;

namespace gestao.ViewModels
{
    //  Comentários:
    // Vou usar essa ViewModel para validar dados da API de fichas. O mesmo subsystema
    // de model validation existe em APIs e quando trabalhamos com views. 
    // Aqui vou usar as mesmas propriedades que o model, mas posso ter nomes diferentes
    public class FichaFuncionalViewModel
    {
        public int fichaid { get; set; }
        [Required]
        [MinLength(10)]
        public string Titulo { get; set; }
         [Required]
        public string  Descricao { get; set; }
        public DateTimeOffset DataFicha { get; set; }
        //public FuncionarioViewModel Funcionario {get; set;}
    }
}