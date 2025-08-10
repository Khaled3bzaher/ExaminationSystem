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
                .ForMember(d=>d.Result, opt => opt.MapFrom(src => src.ExamResult != null ? src.ExamResult.Result : 0))
                .ForMember(s => s.ProfilePictureUrl, opt => opt.MapFrom(src => src.Student.ProfilePictureUrl))
                .ForMember(s => s.SubjectName, opt => opt.MapFrom(src => src.Subject.Name))
                .ForMember(s => s.StudentName, opt => opt.MapFrom(src => src.Student.FullName))
                .ForMember(s=>s.ExamDateTime,opt=>opt.MapFrom(src=>src.CreatedAt));
                ;
            CreateMap<ExamQuestionDTO, ExamQuestion>();
            CreateMap<StudentExamDTO, StudentExam>()
                .ForMember(s => s.ExamQuestions, opt => opt.MapFrom(src => src.Questions))
                ;
            CreateMap<StudentExam, StartExamNotificationDTO>()
                .ForMember(s => s.SubjectName, opt => opt.MapFrom(src => src.Subject.Name))
                .ForMember(s => s.StudentName, opt => opt.MapFrom(src => src.Student.FullName));

            CreateMap<QuestionChoice, ExamQuestionChoiceResponse>();


            CreateMap<ExamQuestion, PreviewExamQuestionResponse>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.QuestionId))
                .ForMember(d => d.Text, opt => opt.MapFrom(s => s.Question.Text))
                .ForMember(d => d.Choices, opt => opt.MapFrom(s => s.Question.Choices))
                .ForMember(d=>d.CorrectAnswerId
                ,opt=>opt.MapFrom(s=>s.Question.Choices
                    .FirstOrDefault(c=>c.QuestionId==s.QuestionId && c.isCorrect)!.Id
                ))
                ;

            CreateMap<StudentExam, PreviewExamResponse>()
                .ForMember(d => d.SubjectName, opt => opt.MapFrom(s => s.Subject.Name))
                .ForMember(d => d.SubjectName, opt => opt.MapFrom(s => s.Subject.Name))
                .ForMember(d => d.Questions, opt => opt.MapFrom(s => s.ExamQuestions))
                .ForMember(d => d.Result, opt => opt.MapFrom(s => s.ExamResult != null? s.ExamResult.Result : 0))
                ;
        }
    }
}
