using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BigServiceNorthwind.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BigServiceNorthwind
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<NorthwindContext>(options => {
				options.UseSqlServer(Configuration.GetConnectionString("BigConnStr"));
			});
			services.AddCors(options =>
			{
				options.AddPolicy("corspolicy", policy =>
				{
					policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyMethod();
				});	
			});
			services.AddControllers().AddJsonOptions(options =>
			{
				// Use the default property (Pascal) casing.
				options.JsonSerializerOptions.PropertyNamingPolicy = null;
			});

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseAuthorization();
			app.UseCors("corspolicy");
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
