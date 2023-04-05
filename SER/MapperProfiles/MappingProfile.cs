using AutoMapper;
using SER.Domain.Entities;
using SER.ViewModel.Customer.Requests;
using SER.ViewModel.Product.Requests;
using SER.ViewModel.Shop.Requests;

namespace SER.MapperProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CustomerCreateRequest, Customer>();
        CreateMap<CustomerUpdateRequest, Customer>();

        CreateMap<ProductCreateRequest, Product>();
        CreateMap<ProductUpdateRequest, Product>();


        CreateMap<ShopCreateRequest, Shop>();
        CreateMap<ShopUpdateRequest, Shop>();
    }
}
