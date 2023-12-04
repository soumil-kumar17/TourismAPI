using Tourism.Repository.DTO;
using Tourism.Repository.Models;
using AutoMapper;

namespace Tourism
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Branch, BranchDto>();
            //CreateMap<BranchDto, Branch>();
            CreateMap<CreateBranchDto, Branch>();
            CreateMap<Branch, CreateBranchDto>();

            CreateMap<UpdateBranchDto, Branch>();
            CreateMap<Branch, UpdateBranchDto>();

            CreateMap<CreateUserProfileDto, UserProfile>();
            CreateMap<UserProfile, UserProfileDto>();

        }
    }
}
