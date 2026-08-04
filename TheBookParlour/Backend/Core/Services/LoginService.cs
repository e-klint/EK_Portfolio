using TheBookParlour.Data.Entities;
using TheBookParlour.Data.Interfaces;
using TheBookParlour.Data.DTO;
using TheBookParlour.Core.Interfaces;


namespace TheBookParlour.Core.Services
{
    public class LoginService: ILoginService
    {   
        private readonly IUserRepo _userRepo;
        private readonly IPasswordHasher _passwordHasher;

        public LoginService(IUserRepo repo, IPasswordHasher hasher){

            _userRepo = repo;
            _passwordHasher = hasher;
        }

        public async Task<User> Handle(LoginRequest request)
        {
            User? dbUser = await _userRepo.GetByUsername(request.UserName);

            if (dbUser is null)
                throw new InvalidOperationException("The user does not exist.");
         

            bool isVerified = _passwordHasher.Verify(request.Password, dbUser.PasswordHash);

            if (!isVerified)
                throw new UnauthorizedAccessException("The password is incorrect.");

            return dbUser; 
        }
    }
}
