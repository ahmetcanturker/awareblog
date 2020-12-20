using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aware.Blog.Domain.EF.Configurations
{
    class BlogPostTagConfiguration : EntityConfiguration<BlogPostTag, int>
    {
        public BlogPostTagConfiguration() : base("BlogPostTags") { }

        public override void Configure(EntityTypeBuilder<BlogPostTag> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.BlogPost)
                .WithMany(x => x.Tags)
                .HasForeignKey(x => x.BlogPostId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Tag)
                .WithMany()
                .HasForeignKey(x => x.TagId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}