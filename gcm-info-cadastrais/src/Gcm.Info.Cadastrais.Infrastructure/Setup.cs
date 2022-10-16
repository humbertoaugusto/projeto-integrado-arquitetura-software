using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Gcm.Info.Cadastrais.Domain.Repositories;
using Gcm.Info.Cadastrais.Infrastructure.Repositories;

namespace Gcm.Info.Cadastrais.Infrastructure
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
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ICategoriaProdutoRepository, CategoriaProdutoRepository>();            
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<ISqlServerDbContext, SqlServerDbContext>();

            return services;
        }
    }
}
