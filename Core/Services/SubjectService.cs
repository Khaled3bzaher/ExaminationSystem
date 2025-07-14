using Domain.Exceptions;
using Domain.Models;
using Services.Specifications;
using Shared.DTOs;
using Shared.QueryParameters;

namespace Services
{
    internal class SubjectService(IUnitOfWork unitOfWork, IMapper mapper) : ISubjectService
    {
        public async Task<APIResponse<string>> CreateSubjectAsync(SubjectDTO subject)
        {
            var subjectMap = mapper.Map<Subject>(subject);
            await unitOfWork.GetRepository<Subject,Guid>().AddAsync(subjectMap);
            if(await unitOfWork.SaveChangesAsync() >0)
                return APIResponse<string>.SuccessResponse(null,message:$"Subject {subject.Name} Successfully Created");

            throw new Exception("Something went wrong While Saving..!");
        }

        public async Task<APIResponse<string>> DeleteSubjectAsync(Guid subjectId)
        {
            var subjectFound = await unitOfWork.GetRepository<Subject, Guid>().GetAsync(subjectId) ?? throw new NotFoundException($"Subject with Id: {subjectId} Not Found..!");

            unitOfWork.GetRepository<Subject, Guid>().Delete(subjectFound);

            if (await unitOfWork.SaveChangesAsync() > 0)
                return APIResponse<string>.SuccessResponse(null, message: $"Subject {subjectFound.Name} Successfully Deleted");

            throw new Exception("Something went wrong While Saving..!");
        }

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
            var subject = await unitOfWork.GetRepository<Subject, Guid>().GetAsync(id)
                ?? throw new NotFoundException($"Subject with Id: {id} Not Found..!")
                ;
            return mapper.Map<SubjectResponse>(subject);
        }

        public async Task<APIResponse<string>> UpdateSubjectAsync(Guid subjectId, SubjectDTO subject)
        {
            var subjectFound = await unitOfWork.GetRepository<Subject, Guid>().GetAsync(subjectId) ?? throw new NotFoundException($"Subject with Id: {subjectId} Not Found..!");
            mapper.Map(subject, subjectFound);

            unitOfWork.GetRepository<Subject, Guid>().Update(subjectFound);

            if (await unitOfWork.SaveChangesAsync() > 0)
                return APIResponse<string>.SuccessResponse(null, message: $"Subject {subject.Name} Successfully Updated");

            throw new Exception("Something went wrong While Saving..!");
        }
    }
}
