﻿

using Domain.Contracts;
using Domain.Models;
using Services.Specifications.Exams;
using Services.Specifications.Subjects;

namespace Services.Repositories
{
    internal class SubjectService(IUnitOfWork unitOfWork, IMapper mapper) : ISubjectService
    {
        public async Task<APIResponse<string>> CreateSubjectAsync(SubjectDTO subject)
        {
            var subjectMap = mapper.Map<Subject>(subject);
            subjectMap.ExamConfiguration = new ExamConfiguration();
            await unitOfWork.GetRepository<Subject,Guid>().AddAsync(subjectMap);
            if(await unitOfWork.SaveChangesAsync() >0)
                return APIResponse<string>.SuccessResponse(null,message:$"Subject {subject.Name} Successfully Created");
            else
                return APIResponse<string>.FailureResponse("Something went wrong While Saving..!");
        }

        public async Task<APIResponse<string>> DeleteSubjectAsync(Guid subjectId)
        {
            var subjectFound = await unitOfWork.GetRepository<Subject, Guid>().GetAsync(subjectId);

            if (subjectFound == null)
                return APIResponse<string>.FailureResponse($"Subject with Id: {subjectId} Not Found..!", (int)HttpStatusCode.NotFound);

            unitOfWork.GetRepository<Subject, Guid>().Delete(subjectFound);

            if (await unitOfWork.SaveChangesAsync() > 0)
                return APIResponse<string>.SuccessResponse(null, message: $"Subject {subjectFound.Name} Successfully Deleted");
            else
                return APIResponse<string>.FailureResponse("Something went wrong While Saving..!");

        }

        public async Task<PaginatedResponse<SubjectResponse>> GetAllSubjectsAsync(SubjectQueryParameters parameters)
        {
            var specifications = new SubjectSpecifications(parameters);
            var subjects = await unitOfWork.GetRepository<Subject, Guid>().GetAllProjectedAsync<SubjectResponse>(specifications);
            //var data =  mapper.Map<IEnumerable<SubjectResponse>>(subjects);
            var pageCount = subjects.Count();
            var totalCount = await unitOfWork.GetRepository<Subject, Guid>().CountAsync(new SubjectSpecificationsCount(parameters));
            return new(parameters.PageIndex, pageCount, totalCount, subjects);
        }

        public async Task<APIResponse<SubjectResponse>> GetSubjectAsync(Guid id)
        {
            var specifications = new SubjectSpecifications(id);

            var subject = await unitOfWork.GetRepository<Subject, Guid>().GetProjectedAsync<SubjectResponse>(specifications);

            if (subject == null)
                return APIResponse<SubjectResponse>.FailureResponse($"Subject with Id: {id} Not Found..!", (int)HttpStatusCode.NotFound);

            return APIResponse<SubjectResponse>.SuccessResponse(subject);
        }

        public async Task<APIResponse<string>> UpdateSubjectAsync(Guid subjectId, SubjectDTO subject)
        {
            var subjectFound = await unitOfWork.GetRepository<Subject, Guid>().GetAsync(subjectId);
            if (subjectFound == null)
                return APIResponse<string>.FailureResponse($"Subject with Id: {subjectId} Not Found..!", (int)HttpStatusCode.NotFound);

            mapper.Map(subject, subjectFound);

            unitOfWork.GetRepository<Subject, Guid>().Update(subjectFound);

            if (await unitOfWork.SaveChangesAsync() > 0)
                return APIResponse<string>.SuccessResponse(null, message: $"Subject {subject.Name} Successfully Updated");
            else
                return APIResponse<string>.FailureResponse("Something went wrong While Saving..!");
        }

        public async Task<APIResponse<string>> UpdateSubjectConfigurationAsync(Guid subjectId, SubjectConfigurationDTO configurationDTO)
        {
            var configurationSpecification = new ExamConfigurationSpecifications(subjectId);
            var subjectConfigurationFound = await unitOfWork.GetRepository<ExamConfiguration, int>().GetAsync(configurationSpecification);
            if (subjectConfigurationFound == null)
                return APIResponse<string>.FailureResponse($"Subject with Id: {subjectId} Not Found..!", (int)HttpStatusCode.NotFound);
            mapper.Map(configurationDTO, subjectConfigurationFound);
            unitOfWork.GetRepository<ExamConfiguration, int>().Update(subjectConfigurationFound);
            if (await unitOfWork.SaveChangesAsync() > 0)
                return APIResponse<string>.SuccessResponse(null, message: $"Subject Configuration Successfully Updated");
            else
                return APIResponse<string>.FailureResponse("Something went wrong While Saving..!");
        }
    }
}
