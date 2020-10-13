using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace gestao.Data
{
    public class CarreiraRepository: ICarreiraRepository
    {
        private readonly AppGestaoContext _context;

        public CarreiraRepository(AppGestaoContext context)
        {
            _context = context;
        }
        
        public IEnumerable<SelectListItem> GetCarreira()
        {
            
           List<SelectListItem> carreiras = _context.Carreiras.AsNoTracking()
           .OrderBy(n => n.NomeCarreira)
           .Select(n => new SelectListItem{
               Value = n.Iso5.ToString(),
               Text = n.NomeCarreira
           }).ToList();
           var carreiraTip = new SelectListItem()
           {
               Value= null,
               Text = "--- Selecione a Carreira ---"
           };
           carreiras.Insert(0, carreiraTip);
           return new SelectList(carreiras, "Value", "Text");
            
            
        }
    }
}