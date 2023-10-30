using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using OCR.Data;
using OCR.Services.Interfaces;
using OCR.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OCR.Data.Repositories.Interfaces;
using OCR.Data.Repositories;

namespace OCR
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
            ConfigureAppSettings(services);
            services.AddControllersWithViews();
            ConfigureDependencyInjection(services);
            ConfigureAppSettings(services);
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
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Picture}/{action=Index}/{id?}");
            });
        }

        public void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IPictureService, PictureService>();
            services.AddScoped<IOCRService, OCRService>();

            services.AddScoped<IPictureRepository, PictureRepository>();

        }

        public void ConfigureAppSettings(IServiceCollection services)
        {
            //Context DB Connection

            services.AddDbContext<OCRContext>(opt =>
                    opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Configurations

        }
    }
}
