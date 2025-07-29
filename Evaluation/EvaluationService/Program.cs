
using EvaluationService.Applications.Services.Implementation;
using EvaluationService.Applications.Services.Interfaces;
using EvaluationService.Domain.Contracts;
using EvaluationService.Infrastructure.Data;
using EvaluationService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EvaluationService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddDbContext<EvaluationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddSingleton<IMessagingService, RabbitMQService>();
            builder.Services.AddScoped<IExamEvaluationService, ExamEvaluationService>();

            builder.Services.AddHostedService<Worker>();

            var host = builder.Build();
            host.Run();
        }
    }
}