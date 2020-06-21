using gestao.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace gestao.Data
{
    public class AppGestaoContext: DbContext
    {
        public AppGestaoContext(DbContextOptions<AppGestaoContext> options): base(options)
        {

        }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<FichaFuncional> Fichas { get; set; }
    }
}