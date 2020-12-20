using Moq;

namespace Aware.Blog.Web.Application.Tests
{
    public static class ItIs
    {
        public static TValue Equal<TValue>(TValue value)
        {
            return It.Is<TValue>(v => v.Equals(value));
        }
    }
}