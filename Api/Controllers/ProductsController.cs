using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductsController : ControllerBase
	{
		[HttpGet]
		public string GetProduct()
		{
			return "hello world";
		}
	}
}
