using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Infrastructure.Data;
using System.Threading.Tasks;

namespace Api;

public class Program
{
	public static async Task Main(string[] args)
	{
		var hostBuilder = CreateHostBuilder(args).Build();

		using(var scope = hostBuilder.Services.CreateScope()){
			var services = scope.ServiceProvider;
			var loggerFactory = services.GetRequiredService<ILoggerFactory>();
			var logger = loggerFactory.CreateLogger<Program>();

			try
			{
				var context = services.GetRequiredService<StoreContext>();
				await context.Database.MigrateAsync();
				logger.LogInformation("Applying migrations.");

				await StoreContextSeed.SeedAsync(context, loggerFactory);
			}
			catch (System.Exception ex)
			{
				logger.LogError(ex, "Error occured during migration");
			}
			logger.LogInformation("Migrations applied successfully.");
		}
		hostBuilder.Run();
	}

	public static IHostBuilder CreateHostBuilder(string[] args) =>
		Host.CreateDefaultBuilder(args)
			.ConfigureWebHostDefaults(webBuilder =>
			{
				webBuilder.UseStartup<Startup>();
			});
}