using StaffTransferCore.Business;
using StaffTransferCore.Service;
using StaffTransferCore.Business.Interfaces;
using StaffTransferCore.Service.Interfaces;
using StaffTransferCore.Models.Configuration;
using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using StaffTransferCore.Service.OperacionesService;
using StaffTransferCore.Service.Valhalla;

namespace StaffTransferCore
{
    internal class Program
    {
        public static IConfiguration Configuration { get; set; }
        public static string AppEnvironment { get; set; }

        private static void Main(string[] args)
        {
            //Configuración para inyección de dependencias
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            //Se configura salida de Log para ejecución principal
            var logger = serviceProvider.GetService<ILoggerFactory>()
                                        .CreateLogger<Program>();
            logger.LogInformation("Inicia proceso STAFF");

            try
            {
                //Inicia proceso de negocio para generar reservas de STAFF y enviarlas a SIOR
                var bookingStaffProcess = serviceProvider.GetService<IBookingStaffProcess>();
                var bookingTask = bookingStaffProcess.Run();

                bookingTask.Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Console.ReadLine();
            }

            logger.LogInformation("Terminó proceso");
            //Console.ReadKey();
        }

        public static void ConfigureServices(IServiceCollection serviceCollection)
        {
            AppEnvironment = Environment.GetEnvironmentVariable("APP_ENVIRONMENT");

            //Interfaces a implementar
            serviceCollection
                .AddSingleton<IBookingStaffProcess, BookingStaffProcess>()
                .AddTransient<IOperacionesService, OperacionesService>()
                .AddTransient<IValhallaService, ValhallaService>();

            //Se agrega archivo de configuración (appsettings)
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), $"appsettings.{AppEnvironment}.json"), optional: true)
                .AddEnvironmentVariables()
                .Build();

            //Se cargan valores de la sección de Appsettings desde el archivo de configuración
            serviceCollection.AddOptions();
            serviceCollection.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            //Se configura  debuggeo por consola con la configuración de la sección Loggins del archivo de configuración
            serviceCollection.
                AddLogging(loggingBuilder => loggingBuilder
                    .AddConsole()
                    .AddDebug()
                    .AddConfiguration(Configuration.GetSection("Logging"))
            );
        }
    }
}