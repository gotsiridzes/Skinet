using Api.Dtos;
using AutoMapper;
using Core.Entities;

namespace Api.Mappings;

public class MapProfile : Profile
{
	public MapProfile()
	{
		CreateMap<Product, ProductDto>()
			.ForMember(
				dest => dest.ProductBrand,
				ops => ops.MapFrom(source => source.ProductBrand.Name))
			.ForMember(
				dest => dest.ProductType,
				ops => ops.MapFrom(source => source.ProductType.Name))
			.ForMember(
				dest => dest.PictureUrl,
				ops => ops.MapFrom<ProductUrlResolver>());
	}
}