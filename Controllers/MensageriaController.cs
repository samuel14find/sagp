//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography.X509Certificates;
//using System.Threading.Tasks;
//using gestao.Data;
//using gestao.Data.Entities;
//using gestao.ViewModels;
//using Microsoft.AspNetCore.Mvc;

//namespace gestao.Controllers
//{
//    public class MensageriaController : Controller
//    {
//        private readonly AppGestaoContext _context;

//        public MensageriaController(AppGestaoContext context)
//        {
//            _context = context;
//        }
//        public IActionResult VisualizarTodas()
//        {
//            //var mensagens = _context.Threads
//            //    .SelectMany(x => x.Mensagens).OrderByDescending(c => c.Data).ToList()
//            //    .GroupBy(y => y.MensagemThreadId).Select(grp => grp.FirstOrDefault()).ToList();
//            var mensagens1 = _context.Mensagens.OrderByDescending(c => c.Data).ToList();
//            return View(mensagens1);
//        }
//        [HttpGet]
//        public IActionResult Retornar(int id)
//        {
//            //var thread = _context.Threads.First(x => x.MensagemThreadId == id)
//            //    .Mensagens.OrderBy(x => x.Data).ToList();
//            var mensagens = _context.Mensagens.Where(x => x.MensagemThreadId == id).OrderBy(x => x.Data).ToList();
            

//            if(mensagens != null)
//            {
//                RetornarViewModel vm = new RetornarViewModel()
//                {
//                    Mensagens = mensagens,
//                    Assunto = mensagens.FirstOrDefault().Assunto,
//                    MensagemThreadId = id
//                };
//                return View(vm);
//            }
//            return View("Index", "App");
//        }
//        [HttpPost]
//        public IActionResult Retornar (int id, string content)
//        {
//            var topico = _context.Mensagens.Where(x => x.MensagemThreadId == id).FirstOrDefault();
//            if(topico != null)
//            {
//                var novaMensagen = new Mensagem();
//                novaMensagen.Assunto = topico.Assunto;
//                novaMensagen.Data = DateTimeOffset.Now;
//                novaMensagen.Conteudo = content;
//                novaMensagen.MensagemThreadId = id;
//                var index = HttpContext.User.Identity.Name.IndexOf("\\") + 1;
//                novaMensagen.Autor = HttpContext.User.Identity.Name.Substring(index);

//                _context.Mensagens.Add(novaMensagen);
//                _context.SaveChanges();
//            }
//            return RedirectToAction("VisualizarTodas");
//        }
//    }
//}
