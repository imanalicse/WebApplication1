using System;
using System.Diagnostics;
using System.Linq;

namespace WebApplication1.Models
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string[] Tags { get; set; }

        private string DebuggerDisplay
        {
            get
            {
                var tagsSummary = "[]";
                if (Tags != null && Tags.Length > 0)
                {
                    const int max = 3;
                    if (Tags.Length <= max) tagsSummary = $"[{string.Join(", ", Tags)}]";
                    else tagsSummary = $"[{string.Join(", ", Tags.Take(max))}..., ({Tags.Length})]";
                }
                return $"{Title} by {Author} ({Year}) Tags={tagsSummary}";
            }
        }

        public override string ToString() => DebuggerDisplay;
    }
}
