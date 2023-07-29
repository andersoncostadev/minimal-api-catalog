using MinimalAPICatalogo.Domain;

namespace MinimalAPICatalogo.Services
{
    public interface ITokenService
    {
        string GenerateToken(string Key, string issuer, string audience, UserModel user);
    }
}
