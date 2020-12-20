using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aware.Blog.Domain.EF.Configurations
{
    class UserConfiguration : EntityConfiguration<User, int>
    {
        public UserConfiguration() : base("Users") { }

        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Username)
                .IsRequired();

            #region EmailAddress
            builder.Property(x => x.EmailAddress)
                .IsRequired();

            builder.Property(x => x.EmailAddressVerificationStatus)
                .IsRequired();

            builder.Property(x => x.EmailAddressVerificationToken)
                .IsRequired(false);

            builder.Property(x => x.EmailAddressVerificationTokenSentTime)
                .IsRequired(false);
            #endregion

            builder.Property(x => x.PasswordHash)
                .IsRequired();

            builder.Property(x => x.LastLoginTime)
                .IsRequired(false);
        }
    }
}