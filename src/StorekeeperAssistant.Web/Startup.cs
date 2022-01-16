using BuildingBlocks.Infrastructure;
using BuildingBlocks.UseCases;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StorekeeperAssistant.DataAccess;
using StorekeeperAssistant.DataAccess.Repositories;
using StorekeeperAssistant.UseCases.Interfaces;
using StorekeeperAssistant.UseCases.InventoryItems.Queries.GetInventoryItems;
using System.Reflection;

namespace StorekeeperAssistant.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        const string ConfigurationKey = "StorekeeperAssistant:ConnectionString";

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StorekeeperAssistant.Web", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed(origin => true) // allow any origin
                        .AllowAnyHeader());
            });

            services.AddScoped<ISqlConnectionFactory>(x => new SqlConnectionFactory(Configuration[ConfigurationKey]));

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                Configuration[ConfigurationKey]
                ,
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                }));

            services.AddMediatR(typeof(GetInventoryItemsQueryHandler));

            services.AddScoped<IMovingRepository, MovingRepository>();
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();
            services.AddScoped<IWarehouseInventoryItemRepository, WarehouseInventoryItemRepository>();
            services.AddScoped<IInventoryItemRepository, InventoryItemRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StorekeeperAssistant.Web v1"));
            }

            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
