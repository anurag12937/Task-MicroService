using System;
using System.Collections.Generic;
using System.Text;

namespace CodingChallenge.Core.Builders.Factory
{ 
    /// <summary>
    /// 
    /// </summary>
    public class ResponseBuilderFactory : IResponseBuilderFactory
    {
        /// <summary>
        /// Gets the builder.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IResponseBuilder<T> GetBuilder<T>()
        {
            return new ResponseBuilder<T>();
        }
    }
}
