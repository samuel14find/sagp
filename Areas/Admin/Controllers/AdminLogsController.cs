using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gestao.Data;
using gestao.Data.Entities;
using gestao.Views.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace gestao.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Chefe do Setor")]
    public class AdminLogsController : Controller
    {
        private readonly AppGestaoContext _appGestaoContext;
        private readonly ILogger<AdminController> _logger;

        public AdminLogsController(AppGestaoContext appGestaoContext,
                               ILogger<AdminController> logger)
        {
            _appGestaoContext = appGestaoContext;
            _logger = logger;
        }
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            ViewBag.Title = "Lista de Logs";
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TituloParm"] = String.IsNullOrEmpty(sortOrder) ? "titulo_desc" : "";
            ViewData["DataLogSortParm"] = sortOrder == "Data" ? "dataLog_desc" : "Data";
            ViewData["CurrentFilter"] = searchString;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            var logs = from l in _appGestaoContext.LogEventos
                       select l;

            if (!String.IsNullOrEmpty(searchString))
            {
                logs = logs.Where(l => l.Message.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "titulo_desc":
                    logs = logs.OrderByDescending(l => l.LogLevel);
                    break;
                case "Data":
                    logs = logs.OrderBy(l => l.CreatedTime);
                    break;
                case "dataLog_desc":
                    logs = logs.OrderByDescending(l => l.CreatedTime);
                    break;
                default:
                    logs = logs.OrderByDescending(l => l.CreatedTime);
                    break;
            }
            int pageSize = 15;
            return View(await PaginatedList<LogEvento>.CreateAsync(logs.AsNoTracking(), pageNumber ?? 1, pageSize));

        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete()
        {
            var logs = from l in _appGestaoContext.LogEventos
                       where l.CreatedTime < DateTime.Now
                       select l;
            _appGestaoContext.LogEventos.RemoveRange(logs);
            await _appGestaoContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
