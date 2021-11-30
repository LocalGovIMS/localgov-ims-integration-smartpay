using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class NotificationAuthenticationException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public NotificationAuthenticationException()
        {
        }

        public NotificationAuthenticationException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public NotificationAuthenticationException(string message, Exception innerException, HttpStatusCode statusCode)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        protected NotificationAuthenticationException(SerializationInfo info, StreamingContext context, HttpStatusCode statusCode)
            : base(info, context)
        {
            StatusCode = statusCode;
        }
    }
}
