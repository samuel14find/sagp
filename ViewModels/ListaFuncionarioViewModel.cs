using System.Collections;
using System.Collections.Generic;
using gestao.Data.Entities;

namespace gestao.ViewModels
{
    public class ListaFuncionarioViewModel
    {
        //  Comentários:
        // O que quero exibir na view ? Quero exibir uma lista com os funcionários
        // mas informando apenas nome, matricula e setor de lotação. Então lá na view eu 
        // especifico apenas isso. 
        public IEnumerable<Funcionario> Funcionarios { get; set; }
    }
}