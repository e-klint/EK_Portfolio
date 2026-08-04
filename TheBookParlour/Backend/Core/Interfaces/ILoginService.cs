using TheBookParlour.Data.DTO;
using TheBookParlour.Data.Entities;

namespace TheBookParlour.Core.Interfaces
{
    public interface ILoginService
    {
        Task<User> Handle(LoginRequest request);
    }
}
