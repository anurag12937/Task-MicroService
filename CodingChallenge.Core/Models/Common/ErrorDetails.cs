using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CodingChallenge.Core.Models.Common
{
    [ExcludeFromCodeCoverage]
    public class ErrorDetails
  {
    /// <summary>
    /// status code
    /// </summary>
    public int StatusCode { get; set; }
    /// <summary>
    /// messege 
    /// </summary>
    public string Message { get; set; }
    /// <summary>
    /// override string 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return JsonSerializer.Serialize(this);
    }
  }
}
