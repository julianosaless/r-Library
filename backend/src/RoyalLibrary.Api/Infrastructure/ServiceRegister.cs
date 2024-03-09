using System.Reflection;

using Microsoft.EntityFrameworkCore;

using RoaylLibrary.Data.Cache;
using RoaylLibrary.Data.DbContext.Client;
using RoyalLibrary.Features.Features.Books.Query;

namespace RoyalLibrary.Api.Infrastructure
{
    public static class ServiceRegister
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllBookByQuery).GetTypeInfo().Assembly));

            services.AddDbContext<RoyalLibraryDbContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionStrings:Default"]);
            });
           
            services.AddDistributedMemoryCache();
            services.Add(ServiceDescriptor.Singleton<IDataCacheService, DataCacheService>());
        }
    }
}
