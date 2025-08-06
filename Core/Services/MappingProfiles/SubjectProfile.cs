namespace Services.MappingProfiles
{
    internal class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<Subject, SubjectResponse>();
            CreateMap<SubjectDTO, Subject>();
            CreateMap<SubjectConfigurationDTO, ExamConfiguration>();
            CreateMap<ExamConfiguration, SubjectConfigurationResponse>()
                .ForMember(d=>d.SubjectName,opt=>opt.MapFrom(s=>s.Subject.Name));
        }
    }
}
