using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aware.Blog.Domain.EF.Configurations
{
    class EntityConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
        where TEntity : Entity<TKey>
    {
        public string TableName { get; }

        protected EntityConfiguration(
            string tableName = null)
        {
            TableName = tableName;
        }

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedTime)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.Property(x => x.DeletedTime)
                .IsRequired(false);

            if (TableName != null)
                builder.ToTable(TableName);
        }
    }
}