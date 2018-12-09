using System;

namespace Core.Exceptions
{
    public class ContractException : Exception
    {
        public ContractException(string message) : base(message)
        {
        }

        public ContractException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
