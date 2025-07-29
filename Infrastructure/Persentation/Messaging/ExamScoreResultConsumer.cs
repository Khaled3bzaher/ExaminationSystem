using ExaminationSystem.Web.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServicesAbstractions.Messaging;
using Shared.DTOs.Exams;

namespace Persentation.Messaging
{
    public class ExamScoreResultConsumer(IMessagingService rabbitMQ,ILogger<ExamScoreResultConsumer> logger, IHubContext<ExamHub> hubContext) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Exam Score Result Consumer started...");
            await rabbitMQ.SubscribeAsync<ExamEvaluatedResponse>(QueuesConstants.EXAMS_RESULT_QUEUE, async (result) =>
            {
                try
                {
                    logger.LogInformation($"Received evaluated exam. {result.StudentName} in {result.SubjectName}, with Score: {result.Score}");
                    await hubContext.Clients.User(result.StudentId.ToString()).SendAsync("ReceiveExamScore", result);

                    await hubContext.Clients.Group("Admins").SendAsync("NewExamResultAvailable", result);

                    logger.LogInformation($"Successfully sent exam result to student {result.StudentId} and admins");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error while receiving evaluated exam.");
                }
            });
        }

    }
}
