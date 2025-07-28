using Domain.Contracts;
using ExaminationSystem.Web.Factories;
using ExaminationSystem.Web.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shared.Authentication;
using Shared.Options;
using System.Security.Claims;
using System.Text;

namespace ExaminationSystem.Web
{
    public static class Extensions
    {
        public static IServiceCollection AddWebApplicationServices(this  IServiceCollection services,IConfiguration configuration)
        {
            services.AddExceptionHandler<GlobalExceptionHandler>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = APIResponseFactory.GenerateAPIValidationResponse;
            });
            ConfigureJWT(services, configuration);

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddCors(
                options => {
                    options.AddPolicy("AllowAngular",
                        policy => policy
                            .WithOrigins("http://localhost:4200")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials()
                        );
                }
                );


            return services;
        }
        public static async Task<WebApplication> InitializeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync();
            return app;
        }
        private static void ConfigureJWT(IServiceCollection services,IConfiguration configuration)
        {
            var jwt = configuration.GetSection("JwtConfig").Get<JWTOptions>();
            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(config =>
                {
                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwt.Issuer,

                        ValidateAudience = true,
                        ValidAudience = jwt.Audience,

                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Secret))
                    };
                });
            services.AddAuthorization(config =>
            {
                config.AddPolicy(AppPolicy.ADMIN_OR_STUDENT_POLICY, policy =>
                {
                policy.RequireAssertion(context =>
                        context.User.HasClaim(c => c.Type == ClaimTypes.Role &&
                        (c.Value == AppRoles.ADMIN || c.Value == AppRoles.STUDENT)));
                });
                config.AddPolicy(AppPolicy.ADMIN_POLICY, policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, AppRoles.ADMIN);
                });
                config.AddPolicy(AppPolicy.STUDENT_POLICY, policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, AppRoles.STUDENT);
                });
            });
        }
    }
}
