using System;
using System.Collections.Generic;

using Aware.Blog.Common;

namespace Aware.Blog.Domain
{
    public class User : Entity<int>
    {
        public string Username { get; set; }

        #region EmailAddress
        public string EmailAddress { get; set; }
        public VerificationStatus EmailAddressVerificationStatus { get; set; }
        public string EmailAddressVerificationToken { get; set; }
        public DateTime? EmailAddressVerificationTokenSentTime { get; set; }
        #endregion

        public string PasswordHash { get; set; }
        public DateTime? LastLoginTime { get; set; }

        public IList<BlogPost> BlogPosts { get; set; }

        public User()
        {
            BlogPosts = new List<BlogPost>();
        }
    }
}