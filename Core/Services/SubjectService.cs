


using Services.Specifications;
using Shared.QueryParameters;

namespace Services
{
    internal class SubjectService(IUnitOfWork unitOfWork,IMapper mapper) : ISubjectService
    {
        public async Task<IEnumerable<SubjectResponse>> GetAllSubjectsAsync(SubjectQueryParameters parameters)
        {
            var specifications = new SubjectSpecifications(parameters);
            var subjects = await unitOfWork.GetRepository<Subject, Guid>().GetAllAsync(specifications);
            return mapper.Map<IEnumerable<SubjectResponse>>(subjects);
        }

        public async Task<SubjectResponse> GetSubjectAsync(Guid id)
        {
            var subject = await unitOfWork.GetRepository<Subject,Guid>().GetAsync(id);
            return mapper.Map<SubjectResponse>(subject);
        }
    }
}
