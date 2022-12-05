using Api.Extensions;
using Api.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

	// This method gets called by the runtime. Use this method to add services to the container.
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

	// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
