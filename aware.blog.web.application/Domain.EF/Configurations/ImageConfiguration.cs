using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aware.Blog.Domain.EF.Configurations
{
    class ImageConfiguration : EntityConfiguration<Image, int>
    {
        public ImageConfiguration() : base("Images") { }

        public override void Configure(EntityTypeBuilder<Image> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Url)
                .IsRequired();

            builder.Property(x => x.Description)
                .IsRequired();
        }
    }
}