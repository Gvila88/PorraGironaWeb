using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PorraGironaWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Per permetre sessions
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Carregar la configuraci? de la connexi?ala base de dades des
            //del fitxer appsettings.json
            ConnectionStrings = Configuration["ConnectionStrings:PorraGironawebContextConnection"];

            services.AddControllersWithViews();
            services.AddMvc(); //Afegit per funcionalitat Identitat
        }
        public static string ConnectionStrings { get; private set; } = "server=localhost;database=porragirona;uid=root";

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();
            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    //pattern: "{controller=Home}/{action=Index}/{id?}");
                    //pattern: "{controller-Puntuacions}/{action=Index}/{id?}");
                     pattern: "{controller=Login}/{action=Index}/{id?}");

                endpoints.MapRazorPages(); //Afegir per funcionalitat Identitat
            });
        }
    }
}
