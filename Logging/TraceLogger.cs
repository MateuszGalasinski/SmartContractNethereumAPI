using Core.Components;
using System;

namespace Logging
{
    public class TraceLogger : ILogger
    {
        public void Info(string message)
        {
            System.Diagnostics.Trace.WriteLine(message);
        }

        public void Error(string message, Exception exception)
        {
            System.Diagnostics.Trace.WriteLine(message + exception.Message);
        }
    }
}
