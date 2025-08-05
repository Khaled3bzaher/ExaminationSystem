
using Persistence.Repositories;
using ServicesAbstractions.Messaging;

namespace Services.Repositories
{
    public class ServiceManager(IUnitOfWork unitOfWork,IMapper mapper,UserManager<ApplicationUser> userManager,IOptions<JWTOptions> jwtOptions,IMessagingService rabbitMQ) : IServiceManager
    {
        private readonly Lazy<ISubjectService> _subjectService= new Lazy<ISubjectService>(() => new SubjectService(unitOfWork,mapper));
        private readonly Lazy<IAuthenticationService> _lazyAuthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager, jwtOptions));
        private readonly Lazy<IQuestionsService> _lazyQuestionsService = new Lazy<IQuestionsService>(() => new QuestionsService(unitOfWork,mapper));
        private readonly Lazy<IStudentService> _lazyStudentService = new Lazy<IStudentService>(() => new StudentService(userManager,unitOfWork));

        private readonly Lazy<IExamService> _lazyExamService = new Lazy<IExamService>(() => new ExamService(unitOfWork,mapper, rabbitMQ));

        private readonly Lazy<IAdminService> _lazyAdminService = new Lazy<IAdminService>(() => new AdminService(unitOfWork));


        public ISubjectService SubjectService => _subjectService.Value;
        public IAuthenticationService AuthenticationService => _lazyAuthenticationService.Value;

        public IStudentService StudentService => _lazyStudentService.Value;
        public IExamService ExamService=> _lazyExamService.Value;
        public IAdminService AdminService => _lazyAdminService.Value;
        public IQuestionsService QuestionsService => _lazyQuestionsService.Value;
    }
}
