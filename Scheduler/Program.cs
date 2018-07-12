using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scheduler.Extensions;
using Microsoft.Extensions.Logging;
using Scheduler.Services;
using Persistance;
using Microsoft.EntityFrameworkCore;
using Application;
using Application.Interfaces;

namespace Scheduler
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var isService = true;

            if (Debugger.IsAttached || args.Contains("--console"))
            {
                isService = false;
            }

            var pathToContentRoot = Directory.GetCurrentDirectory();

            if (isService)
            {
                var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                pathToContentRoot = Path.GetDirectoryName(pathToExe);
            }

            var host = new HostBuilder()
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.SetBasePath(pathToContentRoot);
                    configApp.AddJsonFile("appsettings.json", optional: true);
                    configApp.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: false);
                    configApp.AddEnvironmentVariables(prefix: "PREFIX_");
                    configApp.AddCommandLine(args);
                })
                 .ConfigureLogging((hostContext, configLogging) =>
                 {
                     configLogging.AddConsole();
                     configLogging.AddDebug();
                 })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddLogging();
                    services.AddTransient<ICleanupService,CleanupService>();
                    services.AddTransient<ICleanupRepository, CleanupRepository>();
                    services.AddDbContext<DataContext>(options =>
                        options.UseSqlServer(hostContext.Configuration.GetConnectionString("SQLConnectionString")));
                    services.AddHostedService<SchduleTaskService>();
                })
                .Build();

            if (isService)
            {
                host.RunAsService();
            }
            else
            {
                host.Run();
            }
        }
    }
}
