using System.Text;
using System.Text.Json;

using Microsoft.Extensions.Caching.Distributed;


namespace RoaylLibrary.Data.Cache
{
    public class DataCacheService(IDistributedCache distributedCache) : IDataCacheService
    {
        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellation = default)
        {
            var currentValueBykey = await distributedCache.GetAsync(key, cancellation);
            if ((currentValueBykey?.Length ?? 0) > 0)
            {
                var currentValue = Encoding.UTF8.GetString(currentValueBykey);
                return JsonSerializer.Deserialize<T>(currentValue);
            }
            return default;
        }

        public async Task SetAsync<T>(string key, T value, DataCacheExpirationOptions expirationOptions, CancellationToken cancellation = default)
        {
            var currentValue = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value));
            await distributedCache.SetAsync(key, currentValue, new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = expirationOptions.AbsoluteExpiration,
                AbsoluteExpirationRelativeToNow = expirationOptions.AbsoluteExpirationRelativeToNow,
                SlidingExpiration = expirationOptions.SlidingExpiration
            }, cancellation);
        }
    }

    public interface IDataCacheService
    {
       
            Task<T> GetAsync<T>(string key, CancellationToken cancellation = default);

            Task SetAsync<T>(string key, T value, DataCacheExpirationOptions expirationOptions, CancellationToken cancellation = default);
    }

    public sealed class DataCacheExpirationOptions
    {
        public DateTimeOffset? AbsoluteExpiration { get; set; }
        public TimeSpan? AbsoluteExpirationRelativeToNow { get; set; }
        public TimeSpan? SlidingExpiration { get; set; }
    }
}
