using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using gestao.Data;
using gestao.Data.Entities;
using gestao.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace gestao
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            this._config = config;

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppGestaoContext>(cfg =>{
                cfg.UseSqlServer(_config.GetConnectionString("StringConexaoBancoGestao"));

            });
            //  Comentarios
            // Aqui trago a funcionalidade de trabalhar com altenticação. 
            // Falo aqui para usar o Entity Framework para armazenar os dados. 
            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<AppGestaoContext>();

            services.AddTransient<Seeder>();

            //  Comentarios:
            //  Lembrar que tenho que criar os AutoMapper profiles, que são uma maneira
            //  de configurar o mapeamento que for usar.
           services.AddAutoMapper(Assembly.GetExecutingAssembly());

           services.AddControllersWithViews()
                   .AddNewtonsoftJson(options =>
                   options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddRazorPages();// Preciso dele porque o Identity usa razor pages  

            services.AddTransient<IRepository, GestaoRepository>();
            services.AddTransient<IProgressoesRepository, ProgressoesRepository>();
            services.AddTransient<ICarreiraRepository, CarreiraRepository>();
            services.AddTransient<IFuncionarioRepository, FuncionarioRepository>();
            services.AddTransient<IFichaFuncionalRepository, FichaFuncionalRepository>();

            //  Comentários:
            // Visto que estou usando Sessions, tenho que registrar esse serviço que 
            // vai me dar acesso ao contexto
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMemoryCache();
            services.AddSession();
            services.AddTransient<IMailService, MockMailService>();

            // Com esse serviço eu pego o carrinho da section.
            // Temos aqui um objeto criado para cada requisição. Se duas pessoas solicitarem 
            // o objeto carrinho de ficha ao mesmo tempo, eles obtem instancias diferentes. 
            // Porque é criado um objeto para cada requisição 
            services.AddScoped(carrinhoFicha => CarrinhoFicha.GetCarrinho(carrinhoFicha));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
                // Abordagens para tratar erro:
                //app.UseStatusCodePages();
                //app.UseStatusCodePages("text/html", "<h1> Status Code Page </h1>");
                //app.UseStatusCodePagesWithRedirects("MinhaPaginaErro/{0}");
                //app.UseStatusCodePagesWithReExecute("MinhaPaginaErro/{0}");
            }
            app.UseStaticFiles();
            app.UseSession();
            app.UseNodeModules();
            app.UseRouting();
            app.UseAuthentication(); 
            app.UseAuthorization();
            app.UseEndpoints(cfg =>
            {
                cfg.MapControllerRoute(
                    name: "funcionarioPorCarreira",
                    pattern: "FuncionarioCarreira/{porCarreira}",
                    defaults: new {Controller="FuncionarioCarreira", Action="Index"}
                );
                
                cfg.MapControllerRoute("Fallback", "{controller}/{action}/{id?}", new { controller = "App", Action = "Index" });
                cfg.MapRazorPages(); //Preciso disso porque o Identity vai usar Razor Pages
            });
        }
    }
}
