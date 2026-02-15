
using AutoMapper;
using ApiBankApp.Data.DTO;
using ApiBankApp.Data.Entities;

namespace ApiBankApp.Data.Profiles
{
    public class AccountProfile: Profile
    {
        public AccountProfile() 
        { 
            CreateMap<Account, AccountDTO>();
            CreateMap<CreateAccountDTO, Account>(); //När kund skapar konto själv
        }
    }
}
