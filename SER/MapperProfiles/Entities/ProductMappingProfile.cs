using AutoMapper;
using SER.Domain.Entities;
using SER.ViewModel.Product.Requests;

namespace SER.MapperProfiles.Entities;

public class ProductMappingProfile : Profile
{
    protected ProductMappingProfile()
    {

        CreateMap<ProductCreateRequest, Product>();
        CreateMap<ProductUpdateRequest, Product>();
    }
}
