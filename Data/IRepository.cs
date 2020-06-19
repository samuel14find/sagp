using System.Collections.Generic;
using gestao.Data.Entities;

namespace gestao.Data
{
    public interface IRepository
    {
        Funcionario Add(Funcionario novoFuncionario);
         int Commit();
         IEnumerable<Funcionario> GetFuncionarios();
        Funcionario GetFuncionarioPorMatricula(string matricula);
    }
}