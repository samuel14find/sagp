using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace gestao.Data
{
    public class ProgressoesRepository: IProgressoesRepository
    {
        private readonly AppGestaoContext _context;

        public ProgressoesRepository(AppGestaoContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetProgressoes()
        {
            List<SelectListItem> progressões = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = null,
                    Text = " "
                }
            };
            return progressões;
        }

        public IEnumerable<SelectListItem> GetProgressoesComCarreira(string iso5)
        {
            if(!String.IsNullOrWhiteSpace(iso5))
            {
                
                    IEnumerable<SelectListItem> progressoes = _context.Progressoes.AsNoTracking()
                    .OrderBy(n => n.NomeProgressao)
                    .Where(n => n.Iso5 == iso5)
                    .Select(n => new SelectListItem{
                        Value = n.ProgressaoCode,
                        Text = n.NomeProgressao
                    }).ToList();
                    return new SelectList(progressoes, "Value", "Text");
                
            }
            return null;
        }
    }
}