using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Gcm.Gestao.Campanha.Marketing.Domain.Repositories;
using Gcm.Gestao.Campanha.Marketing.Infrastructure.Repositories;
using Gcm.Gestao.Campanha.Marketing.Infrastructure.Gateways.Interfaces;
using Gcm.Gestao.Campanha.Marketing.Infrastructure.Gateways;
using System;

namespace Gcm.Gestao.Campanha.Marketing.Infrastructure
{
    /// <summary>
    /// Setup Infrastructure
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Setup
    {
        /// <summary>
        /// Serviços de Domínio da Infrastructure
        /// </summary>
        public static IServiceCollection AddInfraServices(this IServiceCollection services)
        {
            services.AddScoped<ICampanhaRepository, CampanhaRepository>();
            services.AddScoped<IAudienciaRepository, AudienciaRepository>();            
            services.AddScoped<IGcmInfoCadastraisGateway, GcmInfoCadastraisGateway>();
            services.AddScoped<ISqlServerDbContext, SqlServerDbContext>();

            AdicionarGcmInfoCadastraisGateway(services);

            return services;
        }

        /// <summary>
        /// Adiciona  o GcmInfoCadastraisGateway à configuração de serviços
        /// </summary>
        /// <param name="services"></param>
        private static void AdicionarGcmInfoCadastraisGateway(IServiceCollection services)
        {
            services.AddHttpClient<IGcmInfoCadastraisGateway, GcmInfoCadastraisGateway>(client =>
            {
                client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("URL_GCM_INFO_CADASTRAIS"));
            });
        }
    }
}
