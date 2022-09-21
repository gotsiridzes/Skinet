using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Specifications;
using Api.Dtos;
using AutoMapper;

namespace Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductsController : ControllerBase
	{
		private readonly IGenericRepository<Product> _productsRepository;
		private readonly IGenericRepository<ProductBrand> _brandsRepository;
		private readonly IGenericRepository<ProductType> _typesRepository;
		private readonly IMapper _mapper;

		public ProductsController(
			IGenericRepository<Product> productsRepository,
			IGenericRepository<ProductBrand> brandsRepository,
			IGenericRepository<ProductType> typesRepository,
			IMapper mapper)
		{
			_productsRepository = productsRepository;
			_brandsRepository = brandsRepository;
			_typesRepository = typesRepository;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductDto>>> ListProducts()
		{
			var spec = new ProductsWithBrandsAndBrandsSpecification();

			var products = await _productsRepository.ListAsync(spec);
			return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetProduct(int id)
		{
			var spec = new ProductsWithBrandsAndBrandsSpecification(id);
			var product = await _productsRepository.GetEntityWithSpecification(spec);

			return Ok(_mapper.Map<ProductDto>(product));
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
