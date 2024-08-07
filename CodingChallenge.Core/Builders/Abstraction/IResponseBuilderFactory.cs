using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CodingChallenge.Core.Builders
{
    /// <summary>
    /// 
    /// </summary>
    public interface IResponseBuilderFactory
    {
        /// <summary>
        /// Gets the builder.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IResponseBuilder<T> GetBuilder<T>();
    }
}
