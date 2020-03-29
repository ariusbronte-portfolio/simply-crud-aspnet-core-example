using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimplyArchitecture.WebApi.DataAccess;

// Some information:
// https://andrewlock.net/running-async-tasks-on-app-startup-in-asp-net-core-3/
// https://docs.microsoft.com/ru-ru/dotnet/architecture/microservices/multi-container-microservice-net-applications/background-tasks-with-ihostedservice

namespace SimplyArchitecture.WebApi.HostedServices
{
    /// <summary>
    ///     Background task for automatically creating a database.
    /// </summary>
#warning Don't use MigratorHostedService in production! It is only for demo!
    public class MigratorHostedService : IHostedService
    {
        /// <inheritdoc cref="System.IServiceProvider"/>
        private readonly IServiceProvider _provider;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SimplyArchitecture.WebApi.HostedServices.MigratorHostedService"/> class.
        /// </summary>
        /// <param name="provider">Defines a mechanism for retrieving a service object, i.e. an object that provides custom support for other objects.</param>
        public MigratorHostedService(IServiceProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(paramName: nameof(provider));
        }

        /// <inheritdoc />
        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            // Create a new scope to retrieve scoped services
            using (var scope = _provider.CreateScope())
            {
                // Get the DbContext instance
                var dbContext = scope.ServiceProvider.GetRequiredService<SimplyArchitectureDbContext>();

                // Do the migration asynchronously
                await dbContext.Database.EnsureCreatedAsync(cancellationToken: cancellationToken);
            }
        }

        /// <inheritdoc />
        public Task StopAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
    }
}