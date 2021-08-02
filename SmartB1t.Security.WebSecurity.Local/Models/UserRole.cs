using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartB1t.Security.WebSecurity.Local
{
    /// <summary>
    /// Represents the assignation to a <see cref="User"/> of an specific <see cref="Role"/>.
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// The Foreign Key referencing the <see cref="User"/> that has been assigned with a role.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// The <see cref="User"/> who have assigned the <see cref="Role"/>.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// The Foreign Key referencing the <see cref="Role"/> that has been assigned to a <see cref="User"/>.
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// The <see cref="Role"/> that has been assigned to an <see cref="User"/>.
        /// </summary>
        public Role Role { get; set; }
    }
}
