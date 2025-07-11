namespace Services
{
    public class ServiceManager(IUnitOfWork unitOfWork,IMapper mapper) : IServiceManager
    {
        private readonly Lazy<ISubjectService> _subjectService= new Lazy<ISubjectService>(() => new SubjectService(unitOfWork,mapper));
        public ISubjectService SubjectService => _subjectService.Value;
    }
}
