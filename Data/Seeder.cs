using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using gestao.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace gestao.Data

//      Comentários:
//  Com essa classe vou "Semear" dados no banco de dados. Em casos mais simples posso usar 
//  o método OnModelCreating() lá na classe AppGestaoContext, usando o .HasData(). 
//  Mas esse método é para casos mais simples. 
//  Dentro do método Seed() a primeira coisa que fiz foi verificar se o banco de 
//  dados existe. Em seguida faço um if para verificar se não existe funcionario na
//  tabela funcionário. 
//  Aqui usamos o pacote Newtonsoft.Json para conseguir trabalhar com o arquivo 
//  json que criei (func.json) com os dados para "semear" no banco. 
//  Com o método utilizado para localizar o arquivo func.json, fazemos a localização dele
//  em RunTime.
//  Depois que terminei aqui, como vamos executá-lo. Como vou conseguir uma instancia 
//  dessa classe e então chamar o Seed()? Verificar lá no Program.cs. 

{
    public class Seeder
    {
        private readonly AppGestaoContext _context;
        private readonly IWebHostEnvironment _hosting;
        public Seeder(AppGestaoContext context, IWebHostEnvironment hosting)
        {
            this._hosting = hosting;
            this._context = context;

        }
        public void SeedDados()
        {
            _context.Database.EnsureCreated(); 
            if(!_context.Funcionarios.Any())
            {
                //É necessário criar dados
                var filepath = Path.Combine(_hosting.ContentRootPath, "Data/func.json");
                var json = File.ReadAllText(filepath);
                var funcionarios = JsonConvert.DeserializeObject<IEnumerable<Funcionario>>(json);
                //Se tá ok eu adiciono na tabela usando AddRange
                _context.Funcionarios.AddRange(funcionarios);
                _context.SaveChanges();

            }
            
        }
    }
}