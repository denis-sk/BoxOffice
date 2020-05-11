using System;

namespace BoxOffice.Application.Common.Exceptions
{
    public class OrderException: Exception
    {
        public OrderException()
            : base()
        {
        }

        public OrderException(string message)
            : base(message)
        {
        }

        public OrderException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        public OrderException(string name, object key)
            : base($"{name} {key}.")
        {
        }
    }
}
