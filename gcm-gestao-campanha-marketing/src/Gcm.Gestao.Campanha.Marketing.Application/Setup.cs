using Microsoft.Extensions.DependencyInjection;
using Gcm.Gestao.Campanha.Marketing.Application.Interfaces;

namespace Gcm.Gestao.Campanha.Marketing.Application
{
    /// <summary>
    /// Setup da Application
    /// </summary>
    public static class Setup
    {
        /// <summary>
        /// Serviços de Domínio da Application
        /// </summary>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {            
            services.AddScoped<ICampanhaApplication, CampanhaApplication>();
            services.AddScoped<IAudienciaApplication, AudienciaApplication>();

            return services;
        }
    }
}
