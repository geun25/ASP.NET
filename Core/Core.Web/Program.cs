using Core.Services.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Core.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var webHost = BuildWebHost(args).Build();

            using (var scope = webHost.Services.CreateScope())
            {
                DBFirstDbInitializer initializer = scope.ServiceProvider
                                                        .GetService<DBFirstDbInitializer>();

                int rowAffected = initializer.PlantSeedData();
            }

            webHost.Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });

        public static IHostBuilder BuildWebHost(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureLogging(builder => builder.AddFile(options =>
            {
                options.LogDirectory = "Logs";  // 로그 저장폴더
                options.FileName = "log-";  // 로그파일접두어. log-20210000.txt
                options.FileSizeLimit = null;   // 로그파일 사이즈 제한(10MB)       
                options.RetainedFileCountLimit = null; // 로그파일 보유갯수(2)
            }))
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
