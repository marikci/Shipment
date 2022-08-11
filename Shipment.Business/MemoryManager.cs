using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shipment.Business
{
    public class MemoryManager: IMemoryManager
    {
        private readonly SemaphoreSlim _cacheLock = new SemaphoreSlim(1);
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<MemoryManager> _logger;

        public MemoryManager(IMemoryCache memoryCache, ILogger<MemoryManager> logger)
        {
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            try
            {

                await _cacheLock.WaitAsync();
                var value = _memoryCache.Get<T>(key);
                _cacheLock.Release();
                return value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"MemoryManager {key} not found");
                throw;
            }
        }

        public async Task SetAsync<T>(string key, T value)
        {
            try
            {
                await _cacheLock.WaitAsync();
                _memoryCache.Set<T>(key, value);
                _cacheLock.Release();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"MemoryManager {key} not found");
                throw;
            }
        }
    }

}
