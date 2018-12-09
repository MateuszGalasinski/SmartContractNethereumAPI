using System;

namespace Core.Exceptions
{
    public class AccountException : Exception
    {
        public AccountException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
