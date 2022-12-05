using Api.Errors;
using Api.Mappings;
using Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Extensions;

public static class ApplicationServicesExtensions
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
		services.AddAutoMapper(typeof(MapProfile));
		services.Configure<ApiBehaviorOptions>(ops =>
		{
			ops.InvalidModelStateResponseFactory = actionContext =>
			{
				var errors = actionContext.ModelState.Where(er => er.Value.Errors.Count > 0).SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToArray();
				var errorsResponse = new ApiValidationErrorResponse
				{
					Errors = errors
				};

				return new BadRequestObjectResult(errorsResponse);
			};
		});
		return services;
	}
}