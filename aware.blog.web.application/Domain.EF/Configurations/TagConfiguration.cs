using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aware.Blog.Domain.EF.Configurations
{
    class TagConfiguration : EntityConfiguration<Tag, int>
    {
        public TagConfiguration() : base("Tags") { }

        public override void Configure(EntityTypeBuilder<Tag> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Uri)
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired();
        }
    }
}