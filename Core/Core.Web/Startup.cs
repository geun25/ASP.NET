using Core.Services.Data;
using Core.Services.Interfaces;
using Core.Services.Svcs;
using Core.Utilities.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
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
            Common.SetDataProtection(services, @"C:\Users\rlaeh\source\repos\geun25\ASP.NET\Core\", "Core", Enums.CryptoType.CngCbc);

            // 의존성 주입을 사용하기 위해서 서비스로 등록
            //IUser 인터페이스에 UserService 클래스 인스턴스 주입
            services.AddScoped<DBFirstDbInitializer>();
            services.AddScoped<IUser, UserService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            //DB접속정보, Migrations 프로젝트 지정
            //services.AddDbContext<CodeFirstDbContext>(options =>
            //            options.UseSqlServer(connectionString: Configuration.GetConnectionString(name: "DefaultConnection"),
            //                                 sqlServerOptionsAction: mig => mig.MigrationsAssembly(assemblyName: "Core.Migrations")));

            //DB접속정보만
            services.AddDbContext<DBFirstDbContext>(options =>
                        options.UseSqlServer(connectionString: Configuration.GetConnectionString(name: "DBFirstDBConnection")));

            //Logging
            services.AddLogging(builder =>
            {
                builder.AddConfiguration(Configuration.GetSection(key: "Logging"));
                builder.AddConsole();
                builder.AddDebug();
            });

            services.AddControllersWithViews();

            //신원보증과 승인권한
            services.AddAuthentication(defaultScheme: CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.AccessDeniedPath = "/Membership/Forbidden"; // 권한없는 사용자가 가는 경로
                        options.LoginPath = "/Membership/Login";
                    });
            services.AddAuthorization();

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.Name = ".Core.Session";
                //세션 제한시간
                options.IdleTimeout = TimeSpan.FromMinutes(30); // 기본값은 20분
            });
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
            app.UseCookiePolicy();

            app.UseRouting();

            //신원보증만
            app.UseAuthentication();

            app.UseAuthorization();

            //세션 지정
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
