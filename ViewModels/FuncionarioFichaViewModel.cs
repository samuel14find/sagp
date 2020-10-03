using gestao.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestao.ViewModels
{
    public class FuncionarioFichaViewModel
    {
        // Nessa View Model eu quero exibir a Ficha e os Funcionarios que possuem essa ficha. 
        // Lembrando que as View Model servem para representar os dados que queremos mostrar na view
        public Ficha Ficha { get; set; }
        public IEnumerable <FichaDetalhe> FichaDetalhe { get; set; }
    }
}
