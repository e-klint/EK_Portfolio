using AutoMapper;
using ApiBankApp.Data.DTO;
using ApiBankApp.Data.Entities;

namespace ApiBankApp.Data.Profiles
{
    public class TransactionProfile: Profile
    {
        public TransactionProfile()
        {
            CreateMap<TransactionDTO, Transaction>(); 
            CreateMap<Transaction, TransactionDTO>();
            // När kund skapar transaktion
            CreateMap<CreateTransactionDTO, Transaction>()
                .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.fromAccountId));
        
        }
    }
}
