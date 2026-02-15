using AutoMapper;
using ApiBankApp.Data.DTO;
using ApiBankApp.Data.Entities;

namespace ApiBankApp.Data.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CreateCustomerDTO, Customer>(); //DTO till entity
            CreateMap<Customer, CustomerDTO>();// Entity till DTO
        }
    }
}
