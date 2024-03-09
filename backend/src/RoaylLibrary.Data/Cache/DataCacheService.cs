using System.Text;

using Microsoft.Extensions.Caching.Distributed;

using Newtonsoft.Json;

namespace RoaylLibrary.Data.Cache
{
    public class DataCacheService(IDistributedCache distributedCache) : IDataCacheService
    {
        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellation = default)
        {
            try
            {
                var currentValueBykey = await distributedCache.GetAsync(key, cancellation);
                if ((currentValueBykey?.Length ?? 0) > 0)
                {
                    var currentValue = Encoding.UTF8.GetString(currentValueBykey);
                    return JsonConvert.DeserializeObject<T>(currentValue);
                }
                return default;
            }
            catch (Exception)
            {

                return default;
            }
        }

        public async Task SetAsync<T>(string key, T value, DataCacheExpirationOptions expirationOptions, CancellationToken cancellation = default)
        {
            try
            {
                var currentValue = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value));
                await distributedCache.SetAsync(key, currentValue, new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = expirationOptions.AbsoluteExpiration,
                    AbsoluteExpirationRelativeToNow = expirationOptions.AbsoluteExpirationRelativeToNow,
                    SlidingExpiration = expirationOptions.SlidingExpiration
                }, cancellation);
            }
            catch (Exception)
            {
             
            }
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
