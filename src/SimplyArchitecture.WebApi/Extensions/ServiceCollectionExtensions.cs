using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SimplyArchitecture.WebApi.Domain;
using SimplyArchitecture.WebApi.Dto;
using SimplyArchitecture.WebApi.HostedServices;

namespace SimplyArchitecture.WebApi.Extensions
{
    /// <summary>
    ///     Extension methods for setting up services in an <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection" />.
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "UnusedMethodReturnValue.Global")]
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Adds Swagger services to the specified <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to add services to.</param>
        /// <exception cref="System.ArgumentNullException">
        ///    The services must not be null.
        /// </exception>
        /// <returns>The <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddSwaggerGenerator(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(paramName: nameof(services));

            services.AddSwaggerGen(setupAction: c =>
            {
                c.SwaggerDoc(name: "v1", info: new OpenApiInfo
                {
                    Title = "SimplyArchitecture",
                    Version = "v1"
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(path1: AppContext.BaseDirectory, path2: xmlFile);
                c.IncludeXmlComments(filePath: xmlPath);
            });

            return services;
        }

        /// <summary>
        ///     Scan classes and register the configuration, mapping, and extensions with the service collection
        /// </summary>
        /// <param name="services">The <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to add services to.</param>
        /// <exception cref="System.ArgumentNullException">
        ///    Thrown if <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/> is nullable
        /// </exception>
        /// <returns>The <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(paramName: nameof(services));

            services.AddAutoMapper(configAction: cfg =>
            {
                cfg.CreateMap<PersonEntity, PersonDto>();
                cfg.CreateMap<PersonDto, PersonEntity>();
            }, typeof(Startup));

            return services;
        }

        /// <summary>
        ///     Defines methods for objects that are managed by the host.
        /// </summary>
        /// <param name="services">The <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to add services to.</param>
        /// <exception cref="System.ArgumentNullException">
        ///    Thrown if <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/> is nullable
        /// </exception>
        /// <returns>The <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddHostServices(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            
            services.AddHostedService<MigratorHostedService>();
            
            return services;
        }
    }
}