using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
	public class ProductsWithBrandsAndBrandsSpecification : BaseSpecification<Product>
	{
		public ProductsWithBrandsAndBrandsSpecification(string sort, int? brandId, int? typeId):base(x =>
			(!brandId.HasValue || x.ProductBrandId == brandId) &&
			(!typeId.HasValue || x.ProductTypeId == typeId))
		{
			AddInclude(x => x.ProductType);
			AddInclude(x => x.ProductBrand);
			AddOrderBy(x => x.Name);

			if (!string.IsNullOrEmpty(sort))
			{
				switch (sort)
				{
					case "priceAsc":
						AddOrderBy(x => x.Price);
						break;
					case "priceDesc":
						AddOrderByDesc(x => x.Price);
						break;
					default:
						AddOrderBy(x => x.Name);
						break;
				}
			}
		}

		public ProductsWithBrandsAndBrandsSpecification(int id) : base(x => x.Id == id)
		{
			AddInclude(x => x.ProductType);
			AddInclude(x => x.ProductBrand);
		}
	}
}
