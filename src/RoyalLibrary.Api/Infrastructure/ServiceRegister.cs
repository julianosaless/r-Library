using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RoaylLibrary.Data.Cache;
using RoaylLibrary.Data.DbContext.Client;
using RoyalLibrary.Features.Features.Books.Query;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RoyalLibrary.Api.Infrastructure
{
    public static class ServiceRegister
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllBookByQuery).GetTypeInfo().Assembly));

            services.AddDbContext<RoyalLibraryDbContext>(options =>
            {
                options.UseInMemoryDatabase("app");
            });
           
            //services.AddDbContext<RoyalLibraryDbContext>(options =>
            //{
            //    //options.UseSqlServer(configuration.GetConnectionString("database"));
            //});

            services.AddDistributedMemoryCache();
            services.Add(ServiceDescriptor.Singleton<IDataCacheService, DataCacheService>());
        }
    }
}
