using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	public class ProductsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
