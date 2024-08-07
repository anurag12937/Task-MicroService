using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Core.Cache
{
    public class CacheService: ICacheService
    {
        private CacheResultModel result;
        private IMemoryCache _cache;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }
        #region RETRIEVE

        /// <summary>
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<CacheResultModel> RetrieveFromCacheAsync(string key)
        {
            result = new CacheResultModel(key);
            try
            {
                await Task.Run(() =>
                {
                    if (_cache.Get(key) == null)
                    {
                        result.CacheStatus = CacheResultModel.CacheStatusOption.DoesNotExists;
                    }
                    else
                    {
                        result.CacheStatus = CacheResultModel.CacheStatusOption.Exists;
                        result.CacheValue = _cache.Get(key);
                    }
                });
            }
            catch (Exception error)
            {
                result.CacheStatus = CacheResultModel.CacheStatusOption.Error;
                result.Error = error;
            }
            return result;
        }
        #endregion
        public async Task<CacheResultModel> SaveToCacheAsync<T>(string key, T objectToCache, int? expirationTimeLimit = null)
        {
            result = new CacheResultModel(key);
            object cacheObject = null;
            try
            {
                await Task.Run(() =>
                {
                    if (!_cache.TryGetValue(key, out cacheObject))
                    {
                        cacheObject = Newtonsoft.Json.JsonConvert.SerializeObject(objectToCache);
                        var memoryCacheEntryOptions = new MemoryCacheEntryOptions();
                        
                        memoryCacheEntryOptions.SetAbsoluteExpiration(TimeSpan.FromMinutes(expirationTimeLimit ?? 180));
                
                        _cache.Set(key, objectToCache, memoryCacheEntryOptions);
                    }
                });
                result.CacheValue = cacheObject;
                result.CacheStatus = CacheResultModel.CacheStatusOption.Cached;
            }
            catch (Exception error)
            {
                result.CacheStatus = CacheResultModel.CacheStatusOption.Error;
                result.Error = error;
            }
            return result;
        }
       

        #region CacheExpired

        /// <summary>
        /// Caches the expired callback.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="reason">The reason.</param>
        /// <param name="state">The state.</param>
        private async void CacheExpired_Callback(object key, object value, EvictionReason reason, object state)
        {
            var existingDataInCache = await RetrieveFromCacheAsync(key.ToString());

            if (existingDataInCache.CacheStatus == CacheResultModel.CacheStatusOption.DoesNotExists)
            {
                await SaveToCacheAsync(key.ToString(), value);
            }
        }
        #endregion
    }
}
