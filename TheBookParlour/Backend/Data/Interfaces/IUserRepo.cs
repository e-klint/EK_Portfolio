using TheBookParlour.Data.Entities;

namespace TheBookParlour.Data.Interfaces
{
    public interface IUserRepo
    {
        Task<bool> UsernameExists(string username);

        Task<User?> GetByUsername(string username);
    }
}
