using System;
using System.Collections.Generic;
using gestao.Data.Entities;

namespace gestao.ViewModels
{
    public class FuncionarioDetalhesViewModel
    {
        // O que quero mostrar nessa view ? Todos os dados dos funcionários
        // incluindo sua carreira.
        // public IEnumerable<Funcionario> Funcionario { get; set; }
        // public IEnumerable<FuncionarioCarreiraDisplayViewModel> FuncionarioCarreira { get; set; }

         public int FuncionarioId { get; set; }

        public string matricula { get; set; }

        public string nome { get; set; }

        public string cargo { get; set; }

        public string setor { get; set; }

        public string email { get; set; }

        public DateTimeOffset? datanomeacao { get; set; }

        public DateTimeOffset? dataposse { get; set; }

        public DateTimeOffset? dataexercicio { get; set; }

        public Guid IdentificadorFuncionarioCarreira { get; set; }
        public string NomeCarreira { get; set; }
        public string NomeProgressao { get; set; }
        public IEnumerable<DateTimeOffset> progressaoPorMerito { get; set; }
        public IEnumerable<DateTimeOffset> progressaoFuncional { get; set; }


    }
}