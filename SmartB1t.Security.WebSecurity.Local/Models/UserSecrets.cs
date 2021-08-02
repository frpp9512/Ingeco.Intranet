using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartB1t.Security.WebSecurity.Local
{
    /// <summary>
    /// Represents a set of secret data of the <see cref="User"/>.
    /// Example: Passwords
    /// </summary>
    public class UserSecrets
    {
        /// <summary>
        /// Primary key identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The password of the user.
        /// THIS VALUE MUST BE ENCRYPTED.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The id of the user that owns this <see cref="UserSecrets"/>.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// The <see cref="User"/> that owns this <see cref="UserSecrets"/>.
        /// </summary>
        public User User { get; set; }
    }
}
