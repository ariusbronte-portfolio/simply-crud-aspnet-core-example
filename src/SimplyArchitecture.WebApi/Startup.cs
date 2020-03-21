using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimplyArchitecture.WebApi.Abstractions;
using SimplyArchitecture.WebApi.DataAccess;
using SimplyArchitecture.WebApi.Extensions;
using SimplyArchitecture.WebApi.Implementations;

namespace SimplyArchitecture.WebApi
{
    /// <summary>
    ///      Configures services and the app's request pipeline.
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///     This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Defines a contract for a collection of service descriptors.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SimplyArchitectureDbContext>(optionsAction: options =>
            {
                options.UseSqlite(connectionString: "Filename=database.db");
            });
            
            services.AddAutoMapper();
            services.AddSwaggerGenerator();
            services.AddScoped(serviceType: typeof(IRepository<>), implementationType: typeof(Repository<>));
            services.AddControllers();
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">
        ///     Provides the mechanisms to configure an application's request pipeline.
        /// </param>
        /// <param name="env">
        ///     Provides information about the web hosting environment an application is running in.
        /// </param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
                // Middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(setupAction: c =>
                {
                    c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "SimplyArchitectureV1");
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(configure: endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
