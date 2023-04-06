using System.Security.Claims;

namespace Lang.Dictionary.Web.Extensions
{
    public static class ClaimPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            var claim = principal.Claims.First(x => x.Type == "Id");
            return Guid.Parse(claim.Value);
        }

        public static string GetEmail(this ClaimsPrincipal principal)
        {
            var claim = principal.Claims.First(x => x.Type == "Email");
            return claim.Value;
        }

        public static string GetName(this ClaimsPrincipal principal)
        {
            var claim = principal.Claims.First(x => x.Type == ClaimTypes.Name);
            return claim.Value;
        }

        public static int GetBaseLanguageId(this ClaimsPrincipal principal)
        {
            var claim = principal.Claims.First(x => x.Type == "BaseLanguageId");
            return int.Parse(claim.Value);
        }

        public static int GetStudyLanguageId(this ClaimsPrincipal principal)
        {
            var claim = principal.Claims.First(x => x.Type == "StudyLanguageId");
            return int.Parse(claim.Value);
        }
    }
}
