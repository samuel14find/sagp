using gestao.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestao.Data
{
    public interface IFichaFuncionalRepository
    {
        void CriarFicha(Ficha ficha);
    }
}
