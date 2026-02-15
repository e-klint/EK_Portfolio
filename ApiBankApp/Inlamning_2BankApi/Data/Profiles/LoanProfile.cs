using AutoMapper;
using ApiBankApp.Data.DTO;
using ApiBankApp.Data.Entities;

namespace ApiBankApp.Data.Profiles
{
    public class LoanProfile: Profile
    {
        public LoanProfile()
        {
            CreateMap<CreateLoanDTO,Loan>(); 
            CreateMap<CreateLoanDTO, Customer>().ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.customerId)); //DTO till entity för customerId
            CreateMap<Loan, LoanResponseDTO>();
        }
    }
}
