using Microsoft.AspNetCore.Identity;

namespace WebFruit.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
        Task<string> GetTokenAsync();
    }
}
