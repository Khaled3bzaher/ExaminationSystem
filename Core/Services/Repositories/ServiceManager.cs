
using Persistence.Repositories;

namespace Services.Repositories
{
    public class ServiceManager(IUnitOfWork unitOfWork,IMapper mapper,UserManager<ApplicationUser> userManager,IOptions<JWTOptions> jwtOptions) : IServiceManager
    {
        private readonly Lazy<ISubjectService> _subjectService= new Lazy<ISubjectService>(() => new SubjectService(unitOfWork,mapper));
        private readonly Lazy<IAuthenticationService> _lazyAuthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager, jwtOptions));
        private readonly Lazy<IQuestionsService> _lazyQuestionsService = new Lazy<IQuestionsService>(() => new QuestionsService(unitOfWork,mapper));
        private readonly Lazy<IStudentService> _lasyStudentService = new Lazy<IStudentService>(() => new StudentService(userManager,unitOfWork));

        private readonly Lazy<IExamService> _lasyExamService = new Lazy<IExamService>(() => new ExamService(unitOfWork,mapper));

        private readonly Lazy<IAdminService> _lasyAdminService = new Lazy<IAdminService>(() => new AdminService(unitOfWork));

        public ISubjectService SubjectService => _subjectService.Value;
        public IAuthenticationService AuthenticationService => _lazyAuthenticationService.Value;

        public IStudentService StudentService => _lasyStudentService.Value;
        public IExamService ExamService=> _lasyExamService.Value;
        public IAdminService AdminService => _lasyAdminService.Value;
        public IQuestionsService QuestionsService => _lazyQuestionsService.Value;
    }
}
