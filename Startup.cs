using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gestao.Data;
using gestao.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddTransient<SeederFuncionario>();
            services.AddControllersWithViews();
            services.AddScoped<IRepository, GestaoRepository>();
            services.AddTransient<IMailService, MockMailService>();
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
            }
            app.UseStaticFiles();
            app.UseNodeModules();
            app.UseRouting();

            app.UseEndpoints(cfg =>
            {
                cfg.MapControllerRoute("Fallback", "{controller}/{action}/{id?}", new { controller = "App", Action = "Index" });
            });
        }
    }
}
