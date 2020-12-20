using System.Collections.Generic;

using Aware.Blog.Common;

namespace Aware.Blog.Contract
{
    public class UserDto : EntityDto<int>
    {
        public string Username { get; set; }

        #region EmailAddress
        public string EmailAddress { get; set; }
        public VerificationStatus EmailAddressVerificationStatus { get; set; }
        #endregion

        public IList<BlogPostDto> BlogPosts { get; set; }
    }
}