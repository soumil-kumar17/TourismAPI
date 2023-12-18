using Tourism.Repository.DTO;
using Tourism.Repository.Models;
using AutoMapper;

namespace Tourism;

internal sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Branch, BranchDto>();
        CreateMap<CreateBranchDto, Branch>();
        CreateMap<Branch, CreateBranchDto>();

        CreateMap<UpdateBranchDto, Branch>();
        CreateMap<Branch, UpdateBranchDto>();

        CreateMap<CreateUserProfileDto, UserProfile>();
        CreateMap<UserProfile, UserProfileDto>();

    }
}
