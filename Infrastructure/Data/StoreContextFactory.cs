using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data
{
	public class StoreContextFactory : IDesignTimeDbContextFactory<StoreContext>
	{
		private readonly IConfiguration _configuration;

		public StoreContextFactory(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public StoreContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<StoreContext>();
			optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));

			return new StoreContext(optionsBuilder.Options);
		}
	}
}
