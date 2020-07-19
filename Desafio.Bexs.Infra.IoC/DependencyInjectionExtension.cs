using Desafio.Bexs.Data.Repositories;
using Desafio.Bexs.Domain.Interfaces;
using Desafio.Bexs.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Desafio.Bexs.Infra.IoC
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection ConfigureDomainServices(this IServiceCollection services, string[] args)
        {
            var dataFile = args[0];

            services.AddSingleton<IRotaRepository, RotaRepository>();
            services.AddSingleton<IAeroportoRepository, AeroportoRepository>();
            services.AddSingleton<IRotaRepository>(x=> new RotaRepository(args[0]));
            services.AddSingleton<IBuscadorMelhorRota, BuscadorMelhorRotaDijkstra>();
            services.AddSingleton<IRotaService, RotaService>();

            return services;
        }
    }
}
