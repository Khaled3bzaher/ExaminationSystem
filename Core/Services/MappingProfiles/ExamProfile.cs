using Shared.DTOs.Exams;

namespace Services.MappingProfiles
{
    public class ExamProfile : Profile
    {
        public ExamProfile()
        {
            CreateMap<QuestionChoice,ExamQuestionChoiceResponse>();
            CreateMap<Question, ExamQuestionResponse>();
            CreateMap<StudentExam, ExamHistoryResponse>()
                .ForMember(s => s.Id, opt => opt.MapFrom(src => src.StudentId))
                .ForMember(s => s.ProfilePictureUrl, opt => opt.MapFrom(src => src.Student.ProfilePictureUrl))
                .ForMember(s => s.SubjectName, opt => opt.MapFrom(src => src.Subject.Name))
                .ForMember(s => s.StudentName, opt => opt.MapFrom(src => src.Student.FullName))
                ;
        }
    }
}
