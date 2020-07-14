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
        Funcionario GetFuncionarioPorNome(string nome);
        IEnumerable<Ficha> GetFichas();
        Funcionario GetFuncionarioPorId(int v);
        Ficha GetFichaPorId(int id);
        //  Comentarios:
        // Com essa assinatura, vou conseguir adicioar qualquer tipo de entidade. 
        // não ficarei restrito à algum tipo. Porque o método que usaremos na 
        // implementação apenas vai pegar os dados e anexá-lo ao contexto 
        void AdicionarEntidade(object model);
        void AdicionarFichaParaFunc(int funcId, Ficha novaFicha);
        Funcionario GetFuncionario(int funcId, bool includeFicha);
        // IEnumerable<CarreiraAdministrativa> GetCarreiraAdm {get;}
        // IEnumerable<CarreiraProfessor> GetCarreiraProf {get;}

        IEnumerable<Funcionario> GetFuncionarioAdm();

        IEnumerable<Funcionario> GetFuncionarioProf();
        
    }
}