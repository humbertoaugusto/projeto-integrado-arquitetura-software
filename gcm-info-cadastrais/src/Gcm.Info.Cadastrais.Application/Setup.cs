using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Gcm.Info.Cadastrais.Application.Interfaces;

namespace Gcm.Info.Cadastrais.Application
{
    /// <summary>
    /// Setup da Application
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Setup
    {
        /// <summary>
        /// Serviços de Domínio da Application
        /// </summary>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IClienteApplication, ClienteApplication>();
            services.AddScoped<ICategoriaProdutoApplication, CategoriaProdutoApplication>();           
            services.AddScoped<IProdutoApplication, ProdutoApplication>();

            return services;
        }
    }
}
