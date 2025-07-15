

using Services.Repositories;
using ServicesAbstractions.Interfaces;

namespace Services
{
    public static class ApplicationServicesRegistration
    {
        /// <summary>
        /// Add Auto Mapper, Fluent Validation And Service Manager
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddAutoMapper(typeof(AssemblyReference).Assembly);
            services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly, includeInternalTypes: true);
            services.AddFluentValidationAutoValidation();
            services.Configure<JWTOptions>(configuration.GetSection("JwtConfig"));
            return services;
        }
    }
}
