using AutoMapper;
using SER.Domain.Entities;
using SER.ViewModel.Customer.Requests;

namespace SER.MapperProfiles.Entities;

public class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        CreateMap<CustomerCreateRequest, Customer>();
        CreateMap<CustomerUpdateRequest, Customer>();
    }
}
