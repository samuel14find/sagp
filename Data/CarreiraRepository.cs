using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace gestao.Data
{
    public class CarreiraRepository: ICarreiraRepository
    {
        
        public IEnumerable<SelectListItem> GetCarreira()
        {
            var connectionstring = "Server=localhost\\SQLEXPRESS;Database=gepteste8;Trusted_Connection=True; MultipleActiveResultSets=true";
            var optionsBuilder = new DbContextOptionsBuilder<AppGestaoContext>();
            optionsBuilder.UseSqlServer(connectionstring);
            using (var _context = new AppGestaoContext(optionsBuilder.Options))
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
}