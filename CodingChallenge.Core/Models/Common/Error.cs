using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace CodingChallenge.Core.Models.Common
{
    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    
    public class Error
  {
    /// <summary>
    /// Gets or sets the user message.
    /// </summary>
    /// <value>
    /// The user message.
    /// </value>
    public string UserMessage { set; get; }
    /// <summary>
    /// Gets or sets the error code.
    /// </summary>
    /// <value>
    /// The error code.
    /// </value>
    public int ErrorCode { set; get; }
    /// <summary>
    /// Gets or sets the dev message.
    /// </summary>
    /// <value>
    /// The dev message.
    /// </value>
    public string DevMessage { set; get; }
    /// <summary>
    /// Gets or sets the path.
    /// </summary>
    /// <value>
    /// The path.
    /// </value>
    public string Path { set; get; }
    /// <summary>
    /// The row number
    /// </summary>
    public int RowNumber { get; set; }
    /// <summary>
    /// The value
    /// </summary>
    public object Value { get; set; }

  }
}
