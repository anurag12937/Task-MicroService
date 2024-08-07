using CodingChallenge.Core.Models;
using CodingChallenge.Core.Models.Common;

namespace CodingChallenge.Core.Builders
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResponseBuilder<T>
    {
        /// <summary>
        /// Adds the success data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        ResponseBuilder<T> AddSuccessData(T data);

        /// <summary>
        /// Adds the error.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        ResponseBuilder<T> AddError(object error, int code = 000);

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        APIResponse<T> Build();
    }
}
