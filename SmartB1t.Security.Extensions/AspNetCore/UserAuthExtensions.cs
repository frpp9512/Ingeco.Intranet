using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using SmartB1t.Security.WebSecurity.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartB1t.Security.Extensions.AspNetCore
{
    public static class UserAuthExtensions
    {
        public static async Task SignInAsync(this User user, HttpContext context, string scheme, bool rememberSession) 
            => await context.SignInAsync(scheme,
                                         GenerateClaimsPrincipal(user),
                                         new AuthenticationProperties { IsPersistent = rememberSession });

        private static ClaimsPrincipal GenerateClaimsPrincipal(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Fullname),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            //user.Roles.ForEach(ur => claims.Add(new Claim(ClaimTypes.Role, ur.Role.Name)));
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Role.Name));
            }
            var claimsIdentity = new ClaimsIdentity(claims, "IngecoWeb");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            return claimsPrincipal;
        }

        public static Guid GetId(this ClaimsPrincipal user)
            => new(user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
    }
}
