using System;

namespace Aware.Blog.Contract
{
    public abstract class EntityDto<TKey>
    {
        public TKey Id { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}