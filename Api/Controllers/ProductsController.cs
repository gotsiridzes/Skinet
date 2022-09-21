using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductsController : ControllerBase
	{
		private readonly IGenericRepository<Product> _productsRepository;
		private readonly IGenericRepository<ProductBrand> _brandsRepository;
		private readonly IGenericRepository<ProductType> _typesRepository;

		public ProductsController(
			IGenericRepository<Product> productsRepository,
			IGenericRepository<ProductBrand> brandsRepository,
			IGenericRepository<ProductType> typesRepository)
		{
			_productsRepository = productsRepository;
			_brandsRepository = brandsRepository;
			_typesRepository = typesRepository;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> ListProducts()
		{
			var products = await _productsRepository.ListAsync(new );
			return Ok(products);
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<ActionResult<IReadOnlyList<Product>>> GetProduct(int id)
		{
			var products = await _productsRepository.GetByIdAsync(id);
			return Ok(products);
		}

		[HttpGet]
		[Route("brands")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> ListProductBrands()
		{
			var productBrands = await _productsRepository.ListAllAsync();
			return Ok(productBrands);
		}

		[HttpGet]
		[Route("types")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> ListProductTypes()
		{
			var productTypes = await _typesRepository.ListAllAsync();
			return Ok(productTypes);
		}
	}
}
