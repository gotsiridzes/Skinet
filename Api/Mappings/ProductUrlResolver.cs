﻿using Api.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace Api.Mappings;

public class ProductUrlResolver : IValueResolver<Product, ProductDto, string>
{
	private readonly IConfiguration _configuration;

	public ProductUrlResolver(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public string Resolve(Product source, ProductDto destination, string productUrl, ResolutionContext context)
	{
		if (!string.IsNullOrEmpty(source.PictureUrl))
		{
			return _configuration["ApiUrl"] + source.PictureUrl;
		}

		return null;
	}
}