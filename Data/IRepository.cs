using gestao.Data.Entities;

namespace gestao.Data
{
    public interface IRepository
    {
        Funcionario Add(Funcionario novoFuncionario);
         int Commit();
    }
}