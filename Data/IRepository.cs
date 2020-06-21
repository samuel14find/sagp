using System.Collections.Generic;
using gestao.Data.Entities;

namespace gestao.Data
{
    public interface IRepository
    {
        Funcionario Add(Funcionario novoFuncionario);
         int Commit();
         IEnumerable<Funcionario> GetFuncionarios(bool includeFichas);
        Funcionario GetFuncionarioPorMatricula(string matricula);
        IEnumerable<FichaFuncional> GetFichas();
        Funcionario GetFuncionarioPorId(int v);
        FichaFuncional GetFichaPorId(int id);
        //  Comentarios:
        // Com essa assinatura, vou conseguir adicioar qualquer tipo de entidade. 
        // não ficarei restrito à algum tipo. Porque o método que usaremos na 
        // implementação apenas vai pegar os dados e anexá-lo ao contexto 
        void AdicionarEntidade(object model);
        void AdicionarFichaParaFunc(int funcId, FichaFuncional novaFicha);
        Funcionario GetFuncionario(int funcId, bool includeFicha);
    }
}