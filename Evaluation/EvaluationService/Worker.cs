
using EvaluationService.Applications.DTOs;
using EvaluationService.Applications.Services.Interfaces;
using EvaluationService.Domain.Constants;
using EvaluationService.Domain.Contracts;

namespace EvaluationService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMessagingService _rabbitMQ;
        private readonly IServiceScopeFactory _scopeFactory;

        public Worker(ILogger<Worker> logger,IMessagingService rabbitMQ, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _rabbitMQ = rabbitMQ;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker started...");

            await _rabbitMQ.SubscribeAsync<StudentExamDTO>(QueuesConstants.EXAMS_SUBMIT_QUEUE, async (studentExam) =>
            {
                try
                {
                    _logger.LogInformation($"Received exam for evaluation. ExamId: {studentExam.ExamId}, SubmittedAt: {studentExam.SubmittedAt}");
                    using var scope = _scopeFactory.CreateScope();
                    var evaluationService = scope.ServiceProvider.GetRequiredService<IExamEvaluationService>();

                    var scoreResultDTO = await evaluationService.EvaluateExamAsync(studentExam);
                    await _rabbitMQ.PublishAsync(QueuesConstants.EXAMS_RESULT_QUEUE, scoreResultDTO);

                    _logger.LogInformation($"ExamId: {studentExam.ExamId}, Score: {scoreResultDTO.Score} and Published to Examination System");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while evaluating exam.");
                }
            });
        }
    }
}
