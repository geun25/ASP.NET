using Core.Services.Data;
using Core.Services.Interfaces;
using Core.Services.Svcs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Web
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
            // ������ ������ ����ϱ� ���ؼ� ���񽺷� ���
            //IUser �������̽��� UserService Ŭ���� �ν��Ͻ� ����
            services.AddScoped<IUser, UserService>();

            //DB��������, Migrations ������Ʈ ����
            //services.AddDbContext<CodeFirstDbContext>(options =>
            //            options.UseSqlServer(connectionString: Configuration.GetConnectionString(name: "DefaultConnection"),
            //                                 sqlServerOptionsAction: mig => mig.MigrationsAssembly(assemblyName: "Core.Migrations")));

            //DB����������
            services.AddDbContext<DBFirstDbContext>(options =>
                        options.UseSqlServer(connectionString: Configuration.GetConnectionString(name: "DBFirstDBConnection")));


            services.AddControllersWithViews();
            //services.AddMvc();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
