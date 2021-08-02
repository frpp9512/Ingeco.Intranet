using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartB1t.Security.WebSecurity.Local
{
    /// <summary>
    /// Represents the <see cref="Role"/> that a <see cref="User"/> can perform in the web, givin it access to a specific set of actions.
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Primary key identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the role.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the activities related to the actual role.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The set of <see cref="UserRole"/> that relates the <see cref="User"/>s that are associated with this <see cref="Role"/>.
        /// </summary>
        public List<UserRole> RoleUsers { get; set; }

        /// <summary>
        /// Defines if the actual <see cref="Role"/> is active for it's use in the web.
        /// </summary>
        public bool Active { get; set; } = true;
    }
}
