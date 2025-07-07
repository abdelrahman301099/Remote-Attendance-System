using Microsoft.Extensions.Caching.Memory;
using NetBlaze.Application.Interfaces.General;

namespace NetBlaze.Infrastructure.GenericMemoryCacheRepository
{
    public class MemoryCacheRepository : IMemoryCacheRepository
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public bool SetItemInCache<TItem, TKey>(TKey key, TItem item, int expiryInDays = 1) where TKey : struct
        {
            try
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromDays(expiryInDays),
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(expiryInDays)
                };

                _memoryCache.Set(key, item, cacheEntryOptions);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public TItem? GetItemFromCache<TItem, TKey>(TKey key) where TKey : struct
        {
            if (_memoryCache.TryGetValue(key, out TItem? item))
            {
                return item;
            }

            return item;
        }
    }
}
