namespace Services.MappingProfiles
{
    internal class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<Subject, SubjectResponse>();
            CreateMap<SubjectDTO, Subject>();
        }
    }
}
