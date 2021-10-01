using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCore.Services.Data;
using NetCore.Services.Interfaces;
using NetCore.Services.Svcs;
using NetCore.Utilities.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.Web
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
            Common.SetDataPortection(services, @"C:\Users\jin yeong\Desktop\ASP.NET\", "NetCore", Enums.CryptoType.CngCbc);

            //IUser 인터페이스에 UserService 클래스 인스턴스 주입
            services.AddScoped<IUser, UserService>(); 

            //DB접속정보, Migrations 프로젝트 지정
            //services.AddDbContext<CodeFirstDbContext>(options => 
            //            options.UseSqlServer(connectionString: Configuration.GetConnectionString(name:"DefaultConnection"),
            //                                 sqlServerOptionsAction:mig => mig.MigrationsAssembly(assemblyName:"NetCore.Migrations")));

            //DB접속정보만
            services.AddDbContext<DBFirstDbContext>(options =>
                        options.UseSqlServer(connectionString: Configuration.GetConnectionString(name:"DBFirstDBConnection")));

            services.AddControllersWithViews();

            //신원보증과 승인권한
            services.AddAuthentication(defaultScheme: CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.AccessDeniedPath = "/Membership/Forbidden"; // 접근제한 경로
                        options.LoginPath = "/Membership/Login";
                    });
            services.AddAuthorization();
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

            //신원보증만
            app.UseAuthentication();

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
