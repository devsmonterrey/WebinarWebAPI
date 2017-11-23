using WebApiToken.Models;

namespace WebApiToken.Interfaces
{
    public interface ITokenServices
    {
        Tokens GenerateToken(int userId);
        bool ValidateToken(string tokenId);
        bool Kill(string tokenId);
        bool DeleteByUserId(int userId);
    }
}