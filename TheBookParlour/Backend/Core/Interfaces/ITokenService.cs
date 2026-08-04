using TheBookParlour.Data.Entities;

namespace TheBookParlour.Core.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
