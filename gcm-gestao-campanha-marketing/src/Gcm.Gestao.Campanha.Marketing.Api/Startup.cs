using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Gcm.Gestao.Campanha.Marketing.Api.Filters;
using Gcm.Gestao.Campanha.Marketing.Api.Logging;
using System;
using System.IO;
using Gcm.Gestao.Campanha.Marketing.Infrastructure;
using Gcm.Gestao.Campanha.Marketing.Application;
using System.Text.Json.Serialization;

namespace Gcm.Gestao.Campanha.Marketing.Api
{
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
            
            services.AddAutoMapper(typeof(CampanhaApplication));
            services.AddAutoMapper(typeof(AudienciaApplication));

            services.AddInfraServices();

            services.AddApplicationServices();

            services.AddLoggingSerilog();

            services.AddHealthChecks();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Gcm.Gestao.Campanha.Marketing",
                    Description = "API - Gcm.Gestao.Campanha.Marketing",
                    Version = "v1"
                });

                var apiPath = Path.Combine(AppContext.BaseDirectory, "Gcm.Gestao.Campanha.Marketing.Api.xml");
                var applicationPath = Path.Combine(AppContext.BaseDirectory, "Gcm.Gestao.Campanha.Marketing.Application.xml");

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

            app.UsePathBase("/Gcm.Gestao.Campanha.Marketing");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/Gcm.Gestao.Campanha.Marketing/swagger/v1/swagger.json", "API Gcm.Gestao.Campanha.Marketing");
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
