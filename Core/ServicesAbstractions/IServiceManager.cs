namespace ServicesAbstractions
{
    public interface IServiceManager
    {
        ISubjectService SubjectService { get; }
        IAuthenticationService AuthenticationService { get; }
    }
}
