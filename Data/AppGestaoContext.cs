using gestao.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;

namespace gestao.Data
{
    //  Comentário:
    // Aqui vou trabalhar com o Identity do Asp Net Core. Esse IdentityUser representa o usuário
    // e ele já tem várias propriedades prontas para usar como Email, UserName, Passowrd etc. 
    // É possível criar uma Classe com algumas propriedades que queremos adicionar, e herdar as do 
    // IdentityUser. 
    public class AppGestaoContext: IdentityDbContext<ApplicationUser>
    {
        // public AppGestaoContext()
        // {
        // }

        public AppGestaoContext(DbContextOptions<AppGestaoContext> options): base(options)
        {

        }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Ficha> Fichas { get; set; }
        public DbSet<FichaDetalhe> FichaDetalhes { get; set; }
        public DbSet<CarrinhoFichaItem> CarrinhoFichaItens {get; set;}
        public DbSet<Carreira> Carreiras {get; set;}
        public DbSet<ProgressaoCarreira> Progressoes {get; set;}
        public DbSet<FuncionarioCarreira> FuncionariosCarreira{get; set;}
        public DbSet<CategoriaTarefa> Categorias { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<LogEvento> LogEventos { get; set; }



        // Fazendo um "seed" de alguns dados para trabalhar com a página inicial da aplicação
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CategoriaTarefa>().HasData(new CategoriaTarefa {Id = 1, Nome = "Alta Prioridade", Descricao = "Categoria de tarefa que exige uma atenção alta" });
            modelBuilder.Entity<CategoriaTarefa>().HasData(new CategoriaTarefa { Id = 2, Nome = "Média Prioridade", Descricao = "Categoria de tarefa que exige uma atenção média" });
            modelBuilder.Entity<CategoriaTarefa>().HasData(new CategoriaTarefa { Id = 3, Nome = "Baixa Prioridade", Descricao = "Categoria de tarefa que exige uma atenção baixa" });
            modelBuilder.Entity<CategoriaTarefa>().HasData(new CategoriaTarefa { Id = 4, Nome = "Urgente", Descricao = "Categoria de tarefa que deve ser feita para atendimento da diretoria" });

            modelBuilder.Entity<Tarefa>().HasData(new Tarefa
            {
                Id = 8,
                Titulo = "Concertar dados do Funcional Airton Falcao na planilha Cadastro Geral",
                Descricao = "O Rg está com número errado. O Cpf não consta na lista. Verificar a possibilidade de inserir outros dasdos",
                DataLimite = DateTimeOffset.Now.AddDays(2),
                Funcionario = "Samuel",
                CategoriaId = 1,
                DataCriacao = DateTimeOffset.Now

            });
            modelBuilder.Entity<Tarefa>().HasData(new Tarefa
            {
                Id = 9,
                Titulo = "Lembrete Importante",
                Descricao = "Lançar o adicional noturno dos vigilantes",
                DataLimite = DateTimeOffset.Now.AddDays(3),
                Funcionario = "Ioná",
                CategoriaId = 3,
                DataCriacao = DateTimeOffset.Now

            });
            modelBuilder.Entity<Tarefa>().HasData(new Tarefa
            {
                Id = 10,
                Titulo = "Ver solicitação da Dona Maria",
                Descricao = "Dona maria solicitou via telefone para tentar alterar o endereço de recebimento de correspondencia",
                DataLimite = DateTimeOffset.Now.AddDays(2),
                Funcionario = "Jaqueline",
                CategoriaId = 2,
                DataCriacao = DateTimeOffset.Now

            });

            modelBuilder.Entity<Carreira>().HasData(new Carreira
            {
                Iso5 = "Admin",
                NomeCarreira = "Técnico Administrativo em Educação"
            });
            modelBuilder.Entity<Carreira>().HasData(new Carreira
            {
                Iso5 = "Prof",
                NomeCarreira = "Magistério do Ensino Básico Técnico e Tecnológico"
            });
           
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DI01",
                NomeProgressao = "DI01-Administrativa",
                Iso5="Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DI02",
                NomeProgressao = "DI02-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DI03",
                NomeProgressao = "DI03-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DI04",
                NomeProgressao = "DI04-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DI05",
                NomeProgressao = "DI05-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DI06",
                NomeProgressao = "DI06-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DI07",
                NomeProgressao = "DI07-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DI08",
                NomeProgressao = "DI08-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DI09",
                NomeProgressao = "DI09-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DI10",
                NomeProgressao = "DI10-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DI11",
                NomeProgressao = "DI11-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DI12",
                NomeProgressao = "DI12-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DI13",
                NomeProgressao = "DI13-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DI14",
                NomeProgressao = "DI14-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DI15",
                NomeProgressao = "DI15-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DI16",
                NomeProgressao = "DI16-Administrativa",
                Iso5 = "Admin"
            });
            

            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DII01",
                NomeProgressao = "DII01-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DII02",
                NomeProgressao = "DII02-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DII03",
                NomeProgressao = "DII03-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DII04",
                NomeProgressao = "DII04-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DII05",
                NomeProgressao = "DII05-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DII06",
                NomeProgressao = "DII06-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DII07",
                NomeProgressao = "DII07-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DII08",
                NomeProgressao = "DII08-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DII09",
                NomeProgressao = "DII09-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DII10",
                NomeProgressao = "DII10-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DII11",
                NomeProgressao = "DII11-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DII12",
                NomeProgressao = "DII12-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DII13",
                NomeProgressao = "DII13-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DII14",
                NomeProgressao = "DII14-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DII15",
                NomeProgressao = "DII15-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DII16",
                NomeProgressao = "DII16-Administrativa",
                Iso5 = "Admin"
            });
            
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIII01",
                NomeProgressao = "DII01-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIII02",
                NomeProgressao = "DII02-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIII03",
                NomeProgressao = "DII03-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIII04",
                NomeProgressao = "DII04-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIII05",
                NomeProgressao = "DIII05-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIII06",
                NomeProgressao = "DIII06-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIII07",
                NomeProgressao = "DIII07-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIII08",
                NomeProgressao = "DIII08-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIII09",
                NomeProgressao = "DIII09-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIII10",
                NomeProgressao = "DIII10-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIII11",
                NomeProgressao = "DIII11-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIII12",
                NomeProgressao = "DIII12-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIII13",
                NomeProgressao = "DIII13-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIII14",
                NomeProgressao = "DIII14-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIII15",
                NomeProgressao = "DIII15-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIII16",
                NomeProgressao = "DIII16-Administrativa",
                Iso5 = "Admin"
            });
            
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIV01",
                NomeProgressao = "DIV01-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIV02",
                NomeProgressao = "DIV02-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIV03",
                NomeProgressao = "DIV03-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIV04",
                NomeProgressao = "DIV04-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIV05",
                NomeProgressao = "DIV05-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIV06",
                NomeProgressao = "DIV06-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIV07",
                NomeProgressao = "DIV07-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIV08",
                NomeProgressao = "DIV08-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIV09",
                NomeProgressao = "DIV09-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIV10",
                NomeProgressao = "DIV10-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIV11",
                NomeProgressao = "DIV11-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIV12",
                NomeProgressao = "DIV12-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIV13",
                NomeProgressao = "DIV13-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIV14",
                NomeProgressao = "DIV14-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIV15",
                NomeProgressao = "DIV15-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIV16",
                NomeProgressao = "DIV16-Administrativa",
                Iso5 = "Admin"
            });

            
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DI1",
                NomeProgressao = "DI1-Magistério",
                Iso5 = "Prof"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DI2",
                NomeProgressao = "DI2-Magistério",
                Iso5 = "Prof"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DII1",
                NomeProgressao = "DII1-Magistério",
                Iso5 = "Prof"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DII2",
                NomeProgressao = "DII2-Magistério",
                Iso5 = "Prof"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIII1",
                NomeProgressao = "DIII1-Magistério",
                Iso5 = "Prof"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIII2",
                NomeProgressao = "DIII2-Magistério",
                Iso5 = "Prof"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIII3",
                NomeProgressao = "DIII3-Magistério",
                Iso5 = "Prof"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIII4",
                NomeProgressao = "DIII4-Magistério",
                Iso5 = "Prof"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIV1",
                NomeProgressao = "DIV1-Magistério",
                Iso5 = "Prof"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIV2",
                NomeProgressao = "DIV2-Magistério",
                Iso5 = "Prof"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIV3",
                NomeProgressao = "DIV3-Magistério",
                Iso5 = "Prof"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "DIV4",
                NomeProgressao = "DIV4-Magistério",
                Iso5 = "Prof"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "Titular",
                NomeProgressao = "Titular-Magistério",
                Iso5 = "Prof"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "Sempro",
                NomeProgressao = "Sem progressao",
                Iso5 = "Terc"
            });

            modelBuilder.Entity<Carreira>().HasData(new Carreira
            {
                Iso5 = "Terc",
                NomeCarreira = "Terceirizado"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EI01",
                NomeProgressao = "EI01-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EI02",
                NomeProgressao = "EI02-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EI03",
                NomeProgressao = "EI03-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EI04",
                NomeProgressao = "EI04-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EI05",
                NomeProgressao = "EI05-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EI06",
                NomeProgressao = "EI06-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EI07",
                NomeProgressao = "EI07-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EI08",
                NomeProgressao = "EI08-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EI09",
                NomeProgressao = "EI09-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EI10",
                NomeProgressao = "EI10-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EI11",
                NomeProgressao = "EI11-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EI12",
                NomeProgressao = "EI12-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EI13",
                NomeProgressao = "EI13-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EI14",
                NomeProgressao = "EI14-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EI15",
                NomeProgressao = "EI15-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EI16",
                NomeProgressao = "EI16-Administrativa",
                Iso5 = "Admin"
            });
         

            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EII01",
                NomeProgressao = "EII01-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EII02",
                NomeProgressao = "EII02-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EII03",
                NomeProgressao = "EII03-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EII04",
                NomeProgressao = "EII04-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EII05",
                NomeProgressao = "EII05-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EII06",
                NomeProgressao = "EII06-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EII07",
                NomeProgressao = "EII07-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EII08",
                NomeProgressao = "EII08-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EII09",
                NomeProgressao = "EII09-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EII10",
                NomeProgressao = "EII10-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EII11",
                NomeProgressao = "EII11-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EII12",
                NomeProgressao = "EII12-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EII13",
                NomeProgressao = "EII13-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EII14",
                NomeProgressao = "EII14-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EII15",
                NomeProgressao = "EII15-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EII16",
                NomeProgressao = "EII16-Administrativa",
                Iso5 = "Admin"
            });
            
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIII01",
                NomeProgressao = "EII01-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIII02",
                NomeProgressao = "EII02-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIII03",
                NomeProgressao = "EII03-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIII04",
                NomeProgressao = "EII04-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIII05",
                NomeProgressao = "EIII05-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIII06",
                NomeProgressao = "EIII06-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIII07",
                NomeProgressao = "EIII07-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIII08",
                NomeProgressao = "EIII08-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIII09",
                NomeProgressao = "EIII09-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIII10",
                NomeProgressao = "EIII10-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIII11",
                NomeProgressao = "EIII11-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIII12",
                NomeProgressao = "EIII12-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIII13",
                NomeProgressao = "EIII13-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIII14",
                NomeProgressao = "EIII14-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIII15",
                NomeProgressao = "EIII15-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIII16",
                NomeProgressao = "EIII16-Administrativa",
                Iso5 = "Admin"
            });
         
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIV01",
                NomeProgressao = "EIV01-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIV02",
                NomeProgressao = "EIV02-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIV03",
                NomeProgressao = "EIV03-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIV04",
                NomeProgressao = "EIV04-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIV05",
                NomeProgressao = "EIV05-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIV06",
                NomeProgressao = "EIV06-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIV07",
                NomeProgressao = "EIV07-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIV08",
                NomeProgressao = "EIV08-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIV09",
                NomeProgressao = "EIV09-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIV10",
                NomeProgressao = "EIV10-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIV11",
                NomeProgressao = "EIV11-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIV12",
                NomeProgressao = "EIV12-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIV13",
                NomeProgressao = "EIV13-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIV14",
                NomeProgressao = "EIV14-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIV15",
                NomeProgressao = "EIV15-Administrativa",
                Iso5 = "Admin"
            });
            modelBuilder.Entity<ProgressaoCarreira>().HasData(new ProgressaoCarreira
            {
                ProgressaoCode = "EIV16",
                NomeProgressao = "EIV16-Administrativa",
                Iso5 = "Admin"
            });

        }


    }
}