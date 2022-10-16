using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Gcm.Info.Cadastrais.Api.Filters;
using Gcm.Info.Cadastrais.Api.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Gcm.Info.Cadastrais.Infrastructure;
using Gcm.Info.Cadastrais.Application;
using System.Text.Json.Serialization;

namespace Gcm.Info.Cadastrais.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddControllers();

            services.AddMvc(options => options.Filters.Add(new DefaultExceptionFilterAttribute()));

            services.AddAutoMapper(typeof(ClienteApplication));
            services.AddAutoMapper(typeof(CategoriaProdutoApplication));            
            services.AddAutoMapper(typeof(ProdutoApplication));

            services.AddInfraServices();

            services.AddApplicationServices();

            services.AddLoggingSerilog();

            services.AddHealthChecks();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Gcm.Info.Cadastrais",
                    Description = "API - Gcm.Info.Cadastrais",
                    Version = "v1"
                });

                var apiPath = Path.Combine(AppContext.BaseDirectory, "Gcm.Info.Cadastrais.Api.xml");
                var applicationPath = Path.Combine(AppContext.BaseDirectory, "Gcm.Info.Cadastrais.Application.xml");

                c.IncludeXmlComments(apiPath);
                c.IncludeXmlComments(applicationPath);
            });

            services.AddControllersWithViews()
                    .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UsePathBase("/Gcm.Info.Cadastrais");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/Gcm.Info.Cadastrais/swagger/v1/swagger.json", "API Gcm.Info.Cadastrais");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
