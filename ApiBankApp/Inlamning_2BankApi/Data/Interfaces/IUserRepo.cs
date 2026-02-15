using ApiBankApp.Data.Entities;

namespace ApiBankApp.Data.Interfaces
{
    public interface IUserRepo
    {   
        //Behövs för att hämta user till login metoden
        Task<User?> GetByUsernameAsync(string username);

        //Lägg till fler metoder senare.
        Task CreateUserAsync(User user);

        Task<User?> GetUserByIdAsync(int userId);
    }
}
