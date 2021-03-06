using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SimplyArchitecture.WebApi
{
    /// <summary>
    ///     Entry point class of programme.
    /// </summary>
    public class Program
    {
        /// <summary>
        ///     Entry method of programme.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args: args).Build().RunAsync();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Microsoft.Extensions.Hosting.HostBuilder"/> class with pre-configured defaults.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        /// <returns>The <see cref="Microsoft.Extensions.Hosting.IHostBuilder"/> so that additional calls can be chained.</returns>
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args: args)
                .ConfigureWebHostDefaults(configure: webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}