namespace ServicesAbstractions.Interfaces
{
    public interface IServiceManager
    {
        ISubjectService SubjectService { get; }
        IAuthenticationService AuthenticationService { get; }
        IQuestionsService QuestionsService { get; }
        IStudentService StudentService { get; }
        IExamService ExamService { get; }
    }
}
