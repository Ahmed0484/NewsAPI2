using Microsoft.AspNetCore.Identity;

namespace NewsAPI.Repositories
{
    public interface ITokenRepo
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
