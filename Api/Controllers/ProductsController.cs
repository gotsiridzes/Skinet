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
using Api.Errors;
using Microsoft.AspNetCore.Http;

namespace Api.Controllers
{
	public class ProductsController : BaseApiController
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
		public async Task<ActionResult<IEnumerable<ProductDto>>> ListProducts(string sort,
			int? brandId, int? typeId)
		{
			var spec = new ProductsWithBrandsAndBrandsSpecification(sort, brandId, typeId);

			var products = await _productsRepository.ListAsync(spec);
			return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetProduct(int id)
		{
			var spec = new ProductsWithBrandsAndBrandsSpecification(id);
			var product = await _productsRepository.GetEntityWithSpecification(spec);

			if (product is null)
			{
				return NotFound(new ApiResponse(404));
			}

			return Ok(_mapper.Map<ProductDto>(product));
		}

		[HttpGet]
		[Route("brands")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> ListProductBrands()
		{
			var productBrands = await _brandsRepository.ListAllAsync();
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
