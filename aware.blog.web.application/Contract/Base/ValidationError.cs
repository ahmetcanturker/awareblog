namespace Aware.Blog.Contract
{
    public class ValidationError : Error
    {
        public string PropertyName { get; set; }
    }
}