using System.Collections.Generic;
using gestao.ViewModels;

namespace gestao.Data
{
    public interface IFuncionarioRepository
    {
         List<FuncionarioCarreiraDisplayViewModel> GetFuncionarios();
         FuncionarioEditViewModel CreateFuncionarioCarreira();
         bool SaveFuncionarioCarreira(FuncionarioEditViewModel funcionarioedit);
         FuncionarioCarreiraDisplayViewModel GetFuncionarioByName(string name);
    }
}