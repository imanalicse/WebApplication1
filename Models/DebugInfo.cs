using System;
using System.Diagnostics;

namespace WebApplication1.Models
{
    // Provide a concise debugger display for instances of DebugInfo
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class DebugInfo
    {
        public string AppName { get; set; }
        public int RequestCount { get; set; }
        public DateTime CurrentTime { get; set; }
        public string TraceId { get; set; }
        public string RequestPath { get; set; }

        // A single property used by the DebuggerDisplay attribute to show a friendly string
        private string DebuggerDisplay => $"{AppName} Requests={RequestCount} Time={CurrentTime:O} Trace={TraceId} Path={RequestPath}";

        public override string ToString() => DebuggerDisplay;
    }
}
