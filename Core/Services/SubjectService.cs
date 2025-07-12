


using Services.Specifications;
using Shared.DTOs;
using Shared.QueryParameters;

namespace Services
{
    internal class SubjectService(IUnitOfWork unitOfWork,IMapper mapper) : ISubjectService
    {
        public async Task<PaginatedResponse<SubjectResponse>> GetAllSubjectsAsync(SubjectQueryParameters parameters)
        {
            var specifications = new SubjectSpecifications(parameters);
            var subjects = await unitOfWork.GetRepository<Subject, Guid>().GetAllAsync(specifications);
            var data =  mapper.Map<IEnumerable<SubjectResponse>>(subjects);
            var pageCount = data.Count();
            var totalCount = await unitOfWork.GetRepository<Subject, Guid>().CountAsync(new SubjectSpecificationsCount(parameters));
            return new(parameters.PageIndex, pageCount, totalCount, data);
        }

        public async Task<SubjectResponse> GetSubjectAsync(Guid id)
        {
            var subject = await unitOfWork.GetRepository<Subject,Guid>().GetAsync(id);
            return mapper.Map<SubjectResponse>(subject);
        }
    }
}
