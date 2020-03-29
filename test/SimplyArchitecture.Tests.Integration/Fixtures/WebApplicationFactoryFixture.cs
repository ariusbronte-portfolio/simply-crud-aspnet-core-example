using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SimplyArchitecture.WebApi;
using SimplyArchitecture.WebApi.DataAccess;
using SimplyArchitecture.WebApi.HostedServices;

namespace SimplyArchitecture.Tests.Integration.Fixtures
{
    /// <inheritdoc />
    public class WebApplicationFactoryFixture<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        /// <inheritdoc />
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<SimplyArchitectureDbContext>));

                services.AddDbContext<SimplyArchitectureDbContext>(optionsAction: options =>
                {
                    options.UseInMemoryDatabase(databaseName: "DataSource=:memory:");
                });

                services.AddHostedService<MigratorHostedService>();
            });
        }
    }

    /// <inheritdoc />
    public class WebApplicationFactoryFixture : WebApplicationFactoryFixture<Startup>
    {
    }
}