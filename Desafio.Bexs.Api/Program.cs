using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Bexs.Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Desafio.Bexs.Infra.IoC;

namespace Desafio.Bex.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection()
                .ConfiguraDataFile(args)
                .ConfigureDomainServices();
            var serviceProvider = services.BuildServiceProvider();
            var rotaService = serviceProvider.GetService<IRotaService>();

            rotaService.CarregarTodasRotas();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
