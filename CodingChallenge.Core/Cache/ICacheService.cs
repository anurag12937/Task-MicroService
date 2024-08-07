namespace CodingChallenge.Core.Cache
{
    public interface ICacheService
    {
        /// <summary>
        /// Saves to cache asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="objectToCache">The object to cache.</param>
        /// <param name="expirationTimeLimit">The expiration time limit.</param>
        /// <returns></returns>
        Task<CacheResultModel> SaveToCacheAsync<T>(string key, T objectToCache, int? expirationTimeLimit = null);

        /// <summary>
        /// Retrieves from cache asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Task<CacheResultModel> RetrieveFromCacheAsync(string key);
    }
}
