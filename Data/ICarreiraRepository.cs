using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace gestao.Data
{
    public interface ICarreiraRepository
    {
         IEnumerable<SelectListItem> GetCarreira();
    }
}