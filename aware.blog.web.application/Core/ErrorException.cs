using System;

namespace Aware.Blog.Core
{
    public class _ErrorException<TErrorCode> : Exception
    {
        public int StatusCode { get; }
        public TErrorCode ErrorCode { get; }

        public _ErrorException(
            int statusCode,
            TErrorCode errorCode,
            string message) : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }
    }

    public class ErrorException<TErrorCode> : _ErrorException<TErrorCode>
        where TErrorCode : Enum
    {
        public ErrorException(
            int statusCode,
            TErrorCode errorCode,
            string message) : base(statusCode, errorCode, message)
        { }
    }

    public class ErrorException : _ErrorException<string>
    {
        public ErrorException(
            int statusCode,
            string errorCode,
            string message) : base(statusCode, errorCode, message)
        { }
    }

}