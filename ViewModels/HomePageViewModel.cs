using gestao.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestao.ViewModels
{
    public class HomePageViewModel
    {
        public IEnumerable<Tarefa> Tarefas { get; set; }
    }
}
