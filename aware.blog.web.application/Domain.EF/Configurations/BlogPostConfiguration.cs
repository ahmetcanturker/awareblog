using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aware.Blog.Domain.EF.Configurations
{
    class BlogPostConfiguration : EntityConfiguration<BlogPost, int>
    {
        public BlogPostConfiguration() : base("BlogPosts") { }

        public override void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.Image)
                .WithOne()
                .HasForeignKey<BlogPost>(x => x.ImageId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Author)
                .WithMany(x => x.BlogPosts)
                .HasForeignKey(x => x.AuthorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.Uri)
                .IsRequired();

            builder.Property(x => x.Title)
                .IsRequired();

            builder.Property(x => x.Description)
                .IsRequired(false);

            builder.Property(x => x.Summary)
                .IsRequired(false);

            builder.Property(x => x.ContentMarkdown)
                .IsRequired();
        }
    }
}