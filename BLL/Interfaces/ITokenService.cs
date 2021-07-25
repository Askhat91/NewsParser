using System.Security.Claims;

namespace BLL.Interfaces
{
    public interface ITokenService
    {
        ClaimsIdentity GetIdentity(string username, string password);
        object Token(ClaimsIdentity identity);
    }
}
