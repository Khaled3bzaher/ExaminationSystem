using Microsoft.Extensions.DependencyInjection;

namespace Services
{
    public static class ApplicationServicesRegistration
    {
        /// <summary>
        /// Add Auto Mapper And Service Manager
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddAutoMapper(typeof(AssemblyReference).Assembly);

            return services;
        }
    }
}
