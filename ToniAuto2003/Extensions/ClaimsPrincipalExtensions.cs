using System.Security.Claims;
using static ToniAuto2003.Core.Constants.RoleConstants;

namespace ToniAuto2003.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(AdminRole);
        }
    }
}
