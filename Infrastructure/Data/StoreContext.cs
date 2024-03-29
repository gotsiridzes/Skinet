﻿using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class StoreContext : DbContext
{
	public DbSet<Product> Products { get; set; }
	public DbSet<ProductBrand> ProductBrands { get; set; }
	public DbSet<ProductType> ProductTypes { get; set; }

	public StoreContext(DbContextOptions options) : base(options)
	{
	}
	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}
