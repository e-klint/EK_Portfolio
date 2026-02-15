using AutoMapper;
using ApiBankApp.Data.DTO;
using ApiBankApp.Data.Entities;

namespace ApiBankApp.Data.Profiles
{
    public class CreateUserAccountProfile : Profile
    {
        public CreateUserAccountProfile() 
        {
            CreateMap<CreateUserAccountDTO, User>(); //När admin skapar användare åt kund
            CreateMap<User, UserDTO>(); 
            CreateMap<CreateUserAccountDTO, Account>(); //När admin skapar konto åt kund
        }
    }
}
