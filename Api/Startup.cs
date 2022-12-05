using Api.Extensions;
using Api.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api;

public class Startup
{
	private readonly IConfiguration _configuration;

	public Startup(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public void ConfigureServices(IServiceCollection services)
	{
		services.AddControllers();
		services.AddDbContext<StoreContext>(ops =>
		{
			ops.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
		});

		services.AddApplicationServices();
		services.AddSwaggerDocumentation();
		services.AddCors(ops =>
		{
			ops.AddPolicy("CorsPolicy", policy =>
			{
				policy
					.AllowAnyHeader()
					.AllowAnyMethod()
					.WithOrigins("https://localhost:4200");
			});
		});
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		app.UseMiddleware<ExceptionMiddleware>();
		app.UseSwaggerDocumentation(env);
		app.UseStatusCodePagesWithReExecute("/errors/{0}");
		app.UseHttpsRedirection();

		app.UseRouting();
		app.UseStaticFiles();
		app.UseAuthorization();
		app.UseCors("CorsPolicy");
		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllers();
		});
	}
}
