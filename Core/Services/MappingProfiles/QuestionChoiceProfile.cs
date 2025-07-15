using Shared.DTOs.QuestionChoices;

namespace Services.MappingProfiles
{
    internal class QuestionChoiceProfile : Profile
    {
        public QuestionChoiceProfile()
        {
            CreateMap<QuestionChoice,QuestionChoiceResponse>();
            CreateMap<QuestionChoiceDTO, QuestionChoice>();
        }
    }
}
