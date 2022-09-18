using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductsController : ControllerBase
	{
		private readonly StoreContext _context;

		public ProductsController(StoreContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			var products = await _context.Products.ToListAsync();
			return Ok(products);
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<ActionResult<IEnumerable<Product>>> GetProduct(int id)
		{
			var products = await _context.Products.FindAsync(id);
			return Ok(products);
		}
	}
}
