using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace GestaoObras
{
    public class Startup
    {
        public Startup(IConfiguration configuracao)
        {
            Configuracao = configuracao;
        }

        public IConfiguration Configuracao { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection servicos)
        {
            servicos.AddControllersWithViews();

            servicos.AddDbContext<GestaoObraDataContexto>(options =>
            options.UseSqlite(Configuracao.GetConnectionString("GestaoObras")));
            //GetConnectionString em appsetting
             //Base de Dados SQlite em GestaoObras/Data
             //GestaoObraDataContexto ORM GestaoObras/Data
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder appMiddleware, IWebHostEnvironment ambiente)
        {
            if (ambiente.IsDevelopment())
            {
                appMiddleware.UseDeveloperExceptionPage();
            }
            else
            {
                appMiddleware.UseExceptionHandler("/Home/Erro");
                //O valor padrão do HSTS é 30 dias. Você pode alterar isso para cenários de produção, consulte https://aka.ms/aspnetcore-hsts.
                appMiddleware.UseHsts();
            }
            appMiddleware.UseHttpsRedirection();
            appMiddleware.UseStaticFiles();

            appMiddleware.UseRouting();

            appMiddleware.UseAuthorization();

            appMiddleware.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
