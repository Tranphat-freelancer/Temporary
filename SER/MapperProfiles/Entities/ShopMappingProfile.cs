using AutoMapper;
using SER.Domain.Entities;
using SER.ViewModel.Shop.Requests;

namespace SER.MapperProfiles.Entities;

public class ShopMappingProfile : Profile
{
    protected ShopMappingProfile()
    {
        CreateMap<ShopCreateRequest, Shop>();
        CreateMap<ShopUpdateRequest, Shop>();
    }
}
