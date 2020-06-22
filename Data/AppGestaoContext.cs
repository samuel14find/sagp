using gestao.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace gestao.Data
{
    //  Comentário:
    // Aqui vou trabalhar com o Identity do Asp Net Core. Esse IdentityUser representa o usuário
    // e ele já tem várias propriedades prontas para usar como Email, UserName, Passowrd etc. 
    // É possível criar uma Classe com algumas propriedades que queremos adicionar, e herdar as do 
    // IdentityUser. 
    public class AppGestaoContext: IdentityDbContext<IdentityUser>
    {
        public AppGestaoContext(DbContextOptions<AppGestaoContext> options): base(options)
        {

        }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<FichaFuncional> Fichas { get; set; }
    }
}