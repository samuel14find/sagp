using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gestao.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace gestao
{
    public class Program
    {
        //  Comentários:
        // Nessa classe vamos usar a SeederFuncionario para "semear" alguns dados no 
        // banco de dados. Isso é para que possar ter esses dados prontos antes 
        // servidor web subir. Observar como o truque foi colocar um método entre o 
        // Build() e o Run().   
        // Desse modo, toda hora que iniciarmos (dotnet run) a aplicação, vai ocorrer 
        // a tentativa de "semear" o banco de dados.
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args)
                .ConfigureLogging(builder =>
                {
                    builder.AddConsole().AddDebug();
                })
                .Build();
           
            ExecutaSeeding(host);
            
            host.Run();
        }

        private static void ExecutaSeeding(IHost host)
        {
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
            var configuration = host.Services.GetRequiredService<IConfiguration>();
            using(var scope = scopeFactory.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetService<Seeder>();
               
                seeder.SeedDados();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(SetupConfiguration)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    
                    webBuilder.UseStartup<Startup>();
                });
        //  Comentarios
        // Aqui vamos criar um arquivo de configuração. Com isso ganhamos a habilidade 
        // de criar outros aquivos de configuração, como um XML se o ambiente onde a 
        // aplicação for hospedada solicitar. 
        private static void SetupConfiguration(HostBuilderContext context, IConfigurationBuilder builder)
        {
            //Remove opcoes de configuração padrão
            builder.Sources.Clear();
            builder.AddJsonFile("config.json", false, true)
                   .AddEnvironmentVariables();
        }
    }
}
