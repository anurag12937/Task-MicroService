using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace CodingChallenge.Core.Models
{
    [ExcludeFromCodeCoverage]
    public class StoryDetail
  {
    /// <summary>
    /// story id
    /// </summary>
    public int id { get; set; }
    /// <summary>
    /// story title
    /// </summary>
    public string title { get; set; }
    /// <summary>
    /// story type
    /// </summary>
    public string type { get; set; }
    /// <summary>
    /// story url
    /// </summary>
    public string url { get; set; }

  }
}
