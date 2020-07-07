using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace gestao.Data
{
    public interface IProgressoesRepository
    {
           IEnumerable<SelectListItem> GetProgressoes();
           IEnumerable<SelectListItem> GetProgressoesComCarreira(string iso5); 
    }
}