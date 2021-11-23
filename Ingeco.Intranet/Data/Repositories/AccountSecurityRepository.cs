using Ingeco.Intranet.Data.Contexts;
using SmartB1t.Security.WebSecurity.Local;
using Microsoft.EntityFrameworkCore;
using SmartB1t.Security.WebSecurity.Local.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartB1t.Security;

namespace Ingeco.Intranet.Data.Repositories
{
    public class AccountSecurityRepository : IAccountSecurityRepository
    {
        private readonly WebDataContext _dataContext;

        public AccountSecurityRepository(WebDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AssignRoleToUserAsync(User user, Role role)
        {
            if (!await CheckIfUserHasRole(user, role))
            {
                var userRole = new UserRole { RoleId = role.Id, UserId = user.Id };
                await _dataContext.AddAsync(userRole);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<bool> AuthenticateUser(User user, string password)
        {
            if (user.Active && !user.PermanentDeactivation)
            {
                var userSecret = await _dataContext.Secrets
                        .Where(us => us.UserId == user.Id)
                        .FirstOrDefaultAsync();
                return AuthUtil.TryAuth(user.Email, ref password, SecurityUtil.SecureString(userSecret.Password));
            }
            return false;
        }

        public async Task<bool> CheckIfUserHasRole(User user, Role role)
            => await _dataContext.UserRoles
                                 .Where(ur => ur.UserId == user.Id && ur.RoleId == role.Id)
                                 .AnyAsync();

        public async Task CreateRoleAsync(Role role)
        {
            if (role is not null)
            {
                await _dataContext.AddAsync(role);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task CreateUserAsync(User user, string password)
        {
            if (user is not null)
            {
                var secrets = new UserSecrets
                {
                    Password = SecurityUtil.B64HashEncrypt(user.Email, password)
                };
                user.Secrets = secrets;
                await _dataContext.AddAsync(user);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<Role> GetRoleAsync(Guid id)
            => await _dataContext.Roles.FindAsync(id);

        public async Task<Role> GetRoleAsync(string name)
            => await _dataContext.Roles.FirstOrDefaultAsync(r => r.Name == name);

        public async Task<IEnumerable<Role>> GetRolesAsync()
            => await _dataContext.Roles.Where(r => r.Active).ToListAsync();

        public async Task<User> GetUserAsync(Guid id, bool loadUserRoles = false)
        {
            var user = await _dataContext.Users.FindAsync(id);
            if (loadUserRoles)
            {
                user.Roles = await GetUserRolesAsync(user);
            }
            return user;
        }

        public async Task<User> GetUserAsync(string email, bool loadUserRoles = false)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user is not null && loadUserRoles)
            {
                user.Roles = await GetUserRolesAsync(user);
            }
            return user;
        }

        public async Task<IEnumerable<User>> GetUsersAsync(bool loadUserRoles = false)
        {
            var users = (await _dataContext.Users.Where(u => !u.PermanentDeactivation)
                                                 .OrderByDescending(u => u.Roles.Count())
                                                 .ToListAsync());
            if (loadUserRoles)
            {
                foreach (var user in users)
                {
                    user.Roles = await GetUserRolesAsync(user);
                }
            }
            return users;
        }

        public async Task<IEnumerable<User>> GetUsersAsync(int startIndex, int count, bool loadUserRoles = false)
        {
            var users = await _dataContext.Users.Where(u => !u.PermanentDeactivation)
                                                .Skip(startIndex)
                                                .Take(count)
                                                .OrderByDescending(u => u.Roles.Count())
                                                .ToListAsync();
            if (loadUserRoles)
            {
                foreach (var user in users)
                {
                    user.Roles = await GetUserRolesAsync(user);
                }
            }
            return users;
        }

        private async Task<IEnumerable<UserRole>> GetUserRolesAsync(User user)
        {
            return user is null ? null : await _dataContext.UserRoles
                                                           .Where(ur => ur.UserId == user.Id)
                                                           .Include(ur => ur.Role)
                                                           .ToListAsync();
        }

        public async Task RemoveRoleAsync(Role role)
        {
            if (role is not null)
            {
                _dataContext.Remove(role);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task RemoveRoleFromUserAsync(User user, Role role)
        {
            if (user is not null && role is not null)
            {
                var userRole = await _dataContext.UserRoles.Where(ur => ur.UserId == user.Id && ur.RoleId == role.Id)
                                                           .FirstOrDefaultAsync();

                if (userRole is not null)
                {
                    _dataContext.Remove(userRole);
                    await _dataContext.SaveChangesAsync();
                }
            }
        }

        public async Task RemoveUserAsync(User user)
        {
            if (user is not null)
            {
                _dataContext.Remove(user);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task SetUserPasswordAsync(User user, string newPassword)
        {
            var secrets = await _dataContext.Secrets.FirstOrDefaultAsync(s => s.UserId == user.Id);
            if (secrets is not null)
            {
                secrets.Password = newPassword;
                _dataContext.Update(secrets);
            }
            else
            {
                secrets = new UserSecrets
                {
                    Password = SecurityUtil.B64HashEncrypt(user.Email, newPassword),
                    User = user
                };
                _dataContext.Add(secrets);
            }
        }

        public async Task UpdateRoleAsync(Role role)
        {
            if (role is not null)
            {
                _dataContext.Update(role);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            if (user is not null)
            {
                _dataContext.Update(user);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<int> GetUsersCount(bool includeInactive = true) 
            => await _dataContext.Users.CountAsync(u => !u.PermanentDeactivation && (u.Active || includeInactive));
    }
}
