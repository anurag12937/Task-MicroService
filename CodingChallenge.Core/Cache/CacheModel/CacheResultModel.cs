public class CacheResultModel
{
    /// <summary>
    /// 
    /// </summary>
    public enum CacheStatusOption
    {
        /// <summary>
        /// The result pending
        /// </summary>
        ResultPending,
        /// <summary>
        /// The chached
        /// </summary>
        Cached,
        /// <summary>
        /// The deleted
        /// </summary>
        Deleted,
        /// <summary>
        /// The does not exists
        /// </summary>
        DoesNotExists,
        /// <summary>
        /// The exists
        /// </summary>
        Exists,
        /// <summary>
        /// The error
        /// </summary>
        Error
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CacheResultModel"/> class.
    /// </summary>
    /// <param name="key">The key.</param>
    public CacheResultModel(string key)
    {
        CacheKey = key;
        CacheValue = string.Empty;
        CacheStatus = CacheStatusOption.ResultPending;
        Error = null;

    }

    /// <summary>
    /// Gets or sets the cache key.
    /// </summary>
    /// <value>
    /// The cache key.
    /// </value>
    public string CacheKey { get; set; }

    /// <summary>
    /// Gets or sets the cache value.
    /// </summary>
    /// <value>
    /// The cache value.
    /// </value>
    public object CacheValue { get; set; }

    /// <summary>
    /// Gets or sets the cache status.
    /// </summary>
    /// <value>
    /// The cache status.
    /// </value>
    public CacheStatusOption CacheStatus { get; set; }

    /// <summary>
    /// Gets or sets the error.
    /// </summary>
    /// <value>
    /// The error.
    /// </value>
    public Exception Error { get; set; }

}