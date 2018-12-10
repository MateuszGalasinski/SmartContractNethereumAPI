using System;

namespace Core.Components
{
    public interface ILogger
    {
        void Info(string message);
        void Error(string message, Exception exception);
    }
}
