using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartB1t.Security.WebSecurity.Local.Interfaces
{
    public interface IAccountSecurityRepository
    {
        #region User CRUD operations

        /// <summary>
        /// Creates a new <see cref="User"/> record with the specified password.
        /// </summary>
        /// <param name="user">The new <see cref="User"/> record.</param>
        /// <param name="password">The password that will be stored in the user's secrets.</param>
        /// <returns></returns>
        Task CreateUserAsync(User user, string password);

        /// <summary>
        /// Get the total users registered.
        /// </summary>
        /// <param name="includeInactive">Defines if the inactive users will be included in count.</param>
        /// <returns>The total of users registered.</returns>
        Task<int> GetUsersCount(bool includeInactive = true);

        /// <summary>
        /// Get all the users registered.
        /// </summary>
        /// <param name="loadUserRoles">Defines if will be loaded the User roles.</param>
        /// <returns>A collection of <see cref="User"/> with all the users registered.</returns>
        Task<IEnumerable<User>> GetUsersAsync(bool loadUserRoles = false);

        /// <summary>
        /// Get all the users registered in the range provided.
        /// </summary>
        /// <param name="startingIndex">The index to start retrieving users.</param>
        /// <param name="count">The amount of users to retrieve.</param>
        /// <param name="loadUserRoles">Defines if will be loaded the User roles.</param>
        /// <returns>A collection of <see cref="User"/> with all the users in range.</returns>
        Task<IEnumerable<User>> GetUsersAsync(int startingIndex, int count, bool loadUserRoles = false);

        /// <summary>
        /// Get the <see cref="User"/> with the specified id.
        /// </summary>
        /// <param name="id">The id of the <see cref="User"/> to retrieve.</param>
        /// <param name="loadUserRoles">Defines if will be loaded the User roles.</param>
        /// <returns>The <see cref="User"/> with the specified id.</returns>
        Task<User> GetUserAsync(Guid id, bool loadUserRoles = false);

        /// <summary>
        /// Get the <see cref="User"/> with the specified email address.
        /// </summary>
        /// <param name="email">The email of the <see cref="User"/> to retrieve.</param>
        /// <param name="loadUserRoles">Defines if will be loaded the User roles.</param>
        /// <returns>The <see cref="User"/> with the specified email.</returns>
        Task<User> GetUserAsync(string email, bool loadUserRoles = false);
        
        /// <summary>
        /// Updates the data of the specified <see cref="User"/>.
        /// </summary>
        /// <param name="user">The <see cref="User"/> to update the info.</param>
        /// <returns></returns>
        Task UpdateUserAsync(User user);

        /// <summary>
        /// Removes the provided <see cref="User"/>.
        /// </summary>
        /// <param name="user">The <see cref="User"/> to remove.</param>
        /// <returns></returns>
        Task RemoveUserAsync(User user);

        /// <summary>
        /// Set the given password to the specified <see cref="User"/>.
        /// </summary>
        /// <param name="user">The <see cref="User"/> to set the given password.</param>
        /// <param name="newPassword">The password to set to the specified <see cref="User"/>.</param>
        /// <returns></returns>
        Task SetUserPasswordAsync(User user, string newPassword);

        #endregion

        #region User oprations

        /// <summary>
        /// Authenticates the given <see cref="User"/> with the specified password.
        /// </summary>
        /// <param name="user">The <see cref="User"/> to authenticate.</param>
        /// <param name="password">The authentication password.</param>
        /// <returns><see langword="true"/> if the authentication succeeded.</returns>
        Task<bool> AuthenticateUser(User email, string password);

        #endregion

        #region Role CRUD operations

        /// <summary>
        /// Create a new <see cref="Role"/> record.
        /// </summary>
        /// <param name="role">The new <see cref="Role"/> to create.</param>
        /// <returns></returns>
        Task CreateRoleAsync(Role role);

        /// <summary>
        /// Get all the roles registered.
        /// </summary>
        /// <returns>A collection of <see cref="Role"/>.</returns>
        Task<IEnumerable<Role>> GetRolesAsync();

        /// <summary>
        /// Get the <see cref="Role"/> with the specified id.
        /// </summary>
        /// <param name="id">The id of the <see cref="Role"/> to retrieve.</param>
        /// <returns>The <see cref="Role"/> with the specified id.</returns>
        Task<Role> GetRoleAsync(Guid id);

        /// <summary>
        /// Get the <see cref="Role"/> with the given name.
        /// </summary>
        /// <param name="name">The name of the <see cref="Role"/> to retrieve.</param>
        /// <returns>The <see cref="Role"/> with the given operation.</returns>
        Task<Role> GetRoleAsync(string name);
        
        /// <summary>
        /// Updates the <see cref="Role"/> data.
        /// </summary>
        /// <param name="role">The <see cref="Role"/> to update the data.</param>
        /// <returns></returns>
        Task UpdateRoleAsync(Role role);

        /// <summary>
        /// Removes the specified <see cref="Role"/>.
        /// </summary>
        /// <param name="role">The <see cref="Role"/> to remove.</param>
        /// <returns></returns>
        Task RemoveRoleAsync(Role role);

        #endregion

        #region User roles operations

        /// <summary>
        /// Check if the given <see cref="User"/> have assigned the specified <see cref="Role"/>.
        /// </summary>
        /// <param name="user">The <see cref="User"/> to check it relationship with the specified <see cref="Role"/>.</param>
        /// <param name="role">The <see cref="Role"/> to check it relationship with the specified <see cref="User"/>.</param>
        /// <returns>Returns <see langword="true"/> if the <see cref="User"/> have assigned the <see cref="Role"/>.</returns>
        Task<bool> CheckIfUserHasRole(User user, Role role);

        /// <summary>
        /// Assign the given <see cref="Role"/> to the specified <see cref="User"/>.
        /// </summary>
        /// <param name="user">The <see cref="User"/> which will be assigned with the given <see cref="Role"/>.</param>
        /// <param name="role">The <see cref="Role"/> to be assigned to the specified <see cref="User"/>.</param>
        /// <returns></returns>
        Task AssignRoleToUserAsync(User user, Role role);

        /// <summary>
        /// Removes the assignation of the given <see cref="Role"/> from the specified <see cref="User"/>.
        /// </summary>
        /// <param name="user">The <see cref="User"/> to remove the <see cref="Role"/> assignation from.</param>
        /// <param name="role">The <see cref="Role"/> to remove it assignation.</param>
        /// <returns></returns>
        Task RemoveRoleFromUserAsync(User user, Role role);

        #endregion
    }
}