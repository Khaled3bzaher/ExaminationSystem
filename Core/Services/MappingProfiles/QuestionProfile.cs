using Shared.DTOs.Questions;

namespace Services.MappingProfiles
{
    internal class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionResponse>();
            CreateMap<CreateQuestionDTO, Question>();
        }
    }
}
