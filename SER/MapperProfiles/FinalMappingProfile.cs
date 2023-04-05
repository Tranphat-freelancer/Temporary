using AutoMapper;

namespace SER.MapperProfiles;

public class FinalMappingProfile : Profile
{
    public FinalMappingProfile()
    {
        new CustomerMappingProfile();
    }
}
