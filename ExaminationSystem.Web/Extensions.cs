using Domain.Contracts;
using ExaminationSystem.Web.Factories;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.Web
{
    public static class Extensions
    {
        public static IServiceCollection AddWebApplicationServices(this  IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = APIResponseFactory.GenerateAPIValidationResponse;
            });
            return services;
        }
        public static async Task<WebApplication> InitializeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync();
            return app;
        }
    }
}
