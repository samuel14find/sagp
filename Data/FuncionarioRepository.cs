using System;
using System.Collections.Generic;
using System.Linq;
using gestao.Data.Entities;
using gestao.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace gestao.Data
{
    public class FuncionarioRepository: IFuncionarioRepository
    {
        private readonly ICarreiraRepository _carreiraRepo;
        private readonly IProgressoesRepository _progressaoRepo;
        private readonly AppGestaoContext _context;
        private readonly IRepository _repo;
        private readonly ILogger<FuncionarioRepository> _logger;

        public FuncionarioRepository(ICarreiraRepository carreiraRepo
                                    ,IProgressoesRepository progressaoRepo,
                                    AppGestaoContext context,
                                    IRepository repo,
                                    ILogger<FuncionarioRepository> logger)
        {
            this._carreiraRepo = carreiraRepo;
            this._progressaoRepo = progressaoRepo;
            this._context = context;
            this._repo = repo;
            _logger = logger;
        }
        
        public List<FuncionarioCarreiraDisplayViewModel> GetFuncionarios()

         {
                    
                    List<FuncionarioCarreira> funcionarios = new List<FuncionarioCarreira>();
                    funcionarios = _context.FuncionariosCarreira.AsNoTracking()
                    .Include(c => c.carreira)
                    .Include( p => p.progressao)
                    .ToList();

                    if(funcionarios != null)
                    {
                        List<FuncionarioCarreiraDisplayViewModel> funcionariosDisplay = new List<FuncionarioCarreiraDisplayViewModel>();
                        foreach(var x in funcionarios)
                        {
                            var funcionarioDisplay = new FuncionarioCarreiraDisplayViewModel()
                            {
                                IdentificadorFuncionarioCarreira = x.IdentificadorFuncionarioCarreira,
                                NomeFuncionario = x.NomeFuncionario,
                                NomeCarreira = x.carreira.NomeCarreira,
                                NomeProgressao = x.progressao.NomeProgressao
                            };
                            funcionariosDisplay.Add(funcionarioDisplay);
                        }
                        return funcionariosDisplay;
                    }
                    return null;
                
         }
         public FuncionarioCarreiraDisplayViewModel GetFuncionarioByName(string name)
         {
             //List<FuncionarioCarreira> funcionario = new List<FuncionarioCarreira>();
             var funcionario = _context.FuncionariosCarreira.AsNoTracking()
             .Include(c => c.carreira)
             .Include( p => p.progressao)
             .Where(f => f.NomeFuncionario == name)
             .FirstOrDefault();

             if(funcionario != null)
             {  
                     var funcionarioDisplay = new FuncionarioCarreiraDisplayViewModel()
                     {
                         IdentificadorFuncionarioCarreira = funcionario.IdentificadorFuncionarioCarreira,
                         NomeFuncionario = funcionario.NomeFuncionario,
                         NomeCarreira = funcionario.carreira.NomeCarreira,
                         NomeProgressao = funcionario.progressao.NomeProgressao
                     };
                     
                 return funcionarioDisplay;
             }
             return null;
         }

         public FuncionarioEditViewModel CreateFuncionarioCarreira()
         {
            //  var carreiraRepo = new CarreiraRepository();
            //  var progressaoRepo = new ProgressoesRepository();
              var funcionario = new FuncionarioEditViewModel()
             {
                 IdentificadorFuncionarioCarreira = Guid.NewGuid().ToString(),
                 Carreiras = _carreiraRepo.GetCarreira(),
                 Progressoes = _progressaoRepo.GetProgressoes()
             };
             return funcionario;

         }

         public bool SaveFuncionarioCarreira(FuncionarioEditViewModel funcionarioedit)

         {
             var funcionarioSelecionato = _repo.GetFuncionarioPorNome(funcionarioedit.NomeFuncionario);
             if(funcionarioedit != null && funcionarioSelecionato != null)
             {      
                         if(Guid.TryParse(funcionarioedit.IdentificadorFuncionarioCarreira, out Guid newGuid))
                         {

                         var funcionarioCarreira = new FuncionarioCarreira()
                         {
                             IdentificadorFuncionarioCarreira = newGuid,
                             NomeFuncionario = funcionarioedit.NomeFuncionario,
                             CarreiraIso5 = funcionarioedit.SelectedCarreiraIso5,
                             ProgressaoCode = funcionarioedit.SelectedProgressaoCode
                         };
                         funcionarioCarreira.carreira = _context.Carreiras.Find(funcionarioedit.SelectedCarreiraIso5);
                         funcionarioCarreira.progressao = _context.Progressoes.Find(funcionarioedit.SelectedProgressaoCode);

                         _context.FuncionariosCarreira.Add(funcionarioCarreira);
                         _context.SaveChanges();
                         return true;
                         }
                     
            }
            _logger.LogInformation($"A busca pelo {funcionarioedit.NomeFuncionario}, retornou vazio");
            return false;
        }
    
    }
}