using Shared.DTOs.Students;

namespace Services.MappingProfiles
{
    internal class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<ApplicationUser, StudentResponse>();
        }
    }
}
