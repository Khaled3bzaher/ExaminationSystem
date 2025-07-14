using Microsoft.Extensions.Configuration;

namespace Services
{
    public class ServiceManager(IUnitOfWork unitOfWork,IMapper mapper,UserManager<ApplicationUser> userManager,IOptions<JWTOptions> jwtOptions) : IServiceManager
    {
        private readonly Lazy<ISubjectService> _subjectService= new Lazy<ISubjectService>(() => new SubjectService(unitOfWork,mapper));
        private readonly Lazy<IAuthenticationService> _lazyAuthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager, jwtOptions));
        public ISubjectService SubjectService => _subjectService.Value;
        public IAuthenticationService AuthenticationService => _lazyAuthenticationService.Value;
    }
}
