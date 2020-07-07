using System.Linq;
using gestao.Data;
using Microsoft.AspNetCore.Mvc;

namespace gestao.Components
{
    //  Comentários
    // Essa ViewComponent vai retornar os FuncionariosCarreira pelo tipo da carreira
    public class FuncionarioCarreiraMenu : ViewComponent
    {
        private readonly AppGestaoContext _context;
        public FuncionarioCarreiraMenu(AppGestaoContext context)
        {
            this._context = context;

        }

        public IViewComponentResult Invoke()
        {
            var carreiras = _context.Carreiras.OrderBy(c => c.NomeCarreira);
            return View(carreiras);
        }
    }
}