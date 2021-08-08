using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartB1t.Security.WebSecurity.Local
{
    /// <summary>
    /// Represents an user of the web.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Primary key identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The blob of the User's profile picture.
        /// </summary>
        public byte[] ProfilePicture { get; set; }

        /// <summary>
        /// Full name of the user.
        /// </summary>
        public string Fullname { get; set; }

        /// <summary>
        /// The actual user position in the organinzation.
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Department where the user works.
        /// </summary>
        public string Department { get; set; }
        
        /// <summary>
        /// The email address used to login.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Defines if the user is actually active for operating in the web.
        /// </summary>
        public bool Active { get; set; } = true;

        /// <summary>
        /// Defines if the user has been permanently removed from the web.
        /// </summary>
        public bool PermanentDeactivation { get; set; }

        /// <summary>
        /// The list of roles assigned to the user.
        /// </summary>
        public IEnumerable<UserRole> Roles { get; set; }

        /// <summary>
        /// The <see cref="UserSecrets"/> database id.
        /// </summary>
        public Guid UserSecretsId { get; set; }
        
        /// <summary>
        /// The <see cref="UserSecrets"/> of the user.
        /// </summary>
        public UserSecrets Secrets { get; set; }
    }
}