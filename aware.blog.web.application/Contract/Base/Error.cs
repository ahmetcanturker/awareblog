namespace Aware.Blog.Contract
{
    public class Error
    {
        public int StatusCode { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}