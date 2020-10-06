using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using gestao.Areas.Identity;
using gestao.Data;
using gestao.Data.Entities;
using gestao.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using gestao.Service.ExtensionLogger;

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
            services.BuildServiceProvider().GetService<AppGestaoContext>().Database.Migrate();
            //  Comentarios
            // Aqui trago a funcionalidade de trabalhar com altenticação. 
            // Falo aqui para usar o Entity Framework para armazenar os dados. 
            services.AddIdentity<ApplicationUser, IdentityRole>(
                options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<AppGestaoContext>()
                    .AddDefaultUI()
                    .AddDefaultTokenProviders();
            //Aqui vou registrar o serviço para poder usar Claims( ver Identity/)
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>,
                ApplicationUserClaimsPrincipalFactory>();

            //Registrando o EmailSender usando o SengGrid
            services.AddTransient<IEmailSender, EmailSender>();

            //Autenticação com google
            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = _config["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = _config["Authentication:Google:ClientSecret"];
            });

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
            

            // Com esse serviço eu pego o carrinho da section.
            // Temos aqui um objeto criado para cada requisição. Se duas pessoas solicitarem 
            // o objeto carrinho de ficha ao mesmo tempo, eles obtem instancias diferentes. 
            // Porque é criado um objeto para cada requisição 
            services.AddScoped(carrinhoFicha => CarrinhoFicha.GetCarrinho(carrinhoFicha));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IWebHostEnvironment env, IServiceProvider serviceProvider,
            ILoggerFactory loggerFactory)
        {
            // Aqui vou trabalhar com logger, mas enviando os log para meu Banco
            loggerFactory.AddContext(LogLevel.Information, _config.GetConnectionString("StringConexaoBancoGestao"));
            loggerFactory.AddContext(LogLevel.Error, _config.GetConnectionString("StringConexaoBancoGestao"));
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

                cfg.MapRazorPages(); //Preciso disso porque o Identity vai usar Razor Pages
                cfg.MapAreaControllerRoute(
                   name: "areaAdministrativa",
                   areaName: "Admin",
                   pattern: "Admin/{controller=Admin}/{action=Index}/{id?}"
                   );
                cfg.MapControllerRoute(
                    name: "funcionarioPorCarreira",
                    pattern: "FuncionarioCarreira/{porCarreira}",
                    defaults: new {Controller="FuncionarioCarreira", Action="Index"}
                );

        

                cfg.MapControllerRoute("Default", "{controller}/{action}/{id?}", new { controller = "App", Action = "Index" });
               

               
            });
            //CreateRoles(serviceProvider).Wait();
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] rolesNames = { "Chefe do Setor", "Assistente do Setor", "Geral" };
            IdentityResult result;
            foreach (var namesRole in rolesNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(namesRole);
                if (!roleExist)
                {
                    result = await roleManager.CreateAsync(new IdentityRole(namesRole));
                }
            }
        }
    }
}
