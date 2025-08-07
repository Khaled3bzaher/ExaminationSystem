using EvaluationService.Applications.DTOs;
using EvaluationService.Applications.Services.Interfaces;
using EvaluationService.Domain.Enums;
using EvaluationService.Domain.Models;
using EvaluationService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EvaluationService.Applications.Services.Implementation
{
    internal class ExamEvaluationService(EvaluationDbContext context) : IExamEvaluationService
    {
        public async Task<ExamEvaluatedDTO> EvaluateExamAsync(StudentExamDTO studentExam)
        {
            try
            {
                var questionsCount = studentExam.Questions.Count;
                var questionsAnswered = studentExam.Questions.Where(q=>q.SelectedChoiceId is not null).ToList();             

                var studentExamRecord = await context.StudentExams
                    .Include(se=>se.Subject)
                    .Include(se=>se.Student)
                    .FirstOrDefaultAsync(se=>se.Id == studentExam.ExamId) ?? throw new Exception("Student exam record not found.");
                

                if (!questionsAnswered.Any())
                {
                    studentExamRecord.ExamStatus = ExamStatus.Failed;
                    context.StudentExams.Update(studentExamRecord);

                    var result = new ExamResult
                    {
                        ExamId = studentExam.ExamId,
                        Result = 0,
                        EvaluatedAt = DateTime.Now
                    };
                    await context.ExamResults.AddAsync(result);
                    await context.SaveChangesAsync();

                    return new ExamEvaluatedDTO
                    {
                        StudentId = studentExamRecord.StudentId,
                        StudentName = studentExamRecord.Student.FullName,
                        SubjectName = studentExamRecord.Subject.Name,
                        Score = 0,
                        ExamStatus = ExamStatus.Failed,
                        TotalQuestions = questionsCount,
                    };
                }
                    

                var questionIds = questionsAnswered.Select(q => q.QuestionId).ToList();

                var questionsFromDb = await context.Questions
                    .Where(q => questionIds.Contains(q.Id))
                    .Select(q => new QuestionResponse
                    {
                        Id = q.Id,
                        CorrectAnswerId = q.Choices
                        .Where(c => c.isCorrect)
                        .Select(c => c.Id)
                        .FirstOrDefault()
                    })
                    .ToListAsync();

                int score = 0;

                foreach (var answeredQuestion in questionsAnswered)
                {
                    var question = questionsFromDb.FirstOrDefault(q => q.Id == answeredQuestion.QuestionId);
                    if(question == null)
                        throw new Exception("Question not found in the database.");

                    if (question.CorrectAnswerId == 0)
                        throw new Exception("Question Don't Have Correct Answer in Database");

                    if (answeredQuestion.SelectedChoiceId == question.CorrectAnswerId)
                    {
                        score++;
                    }
                }
                if (score > (questionsCount / 2))
                    studentExamRecord.ExamStatus = ExamStatus.Success;
                else
                    studentExamRecord.ExamStatus = ExamStatus.Failed;

                context.StudentExams.Update(studentExamRecord);

                var examResult = new ExamResult
                {
                    ExamId = studentExam.ExamId,
                    Result = score,
                    EvaluatedAt = DateTime.Now
                };
                await context.ExamResults.AddAsync(examResult);
                await context.SaveChangesAsync();

                return new ExamEvaluatedDTO
                {
                    StudentId = studentExamRecord.StudentId,
                    StudentName = studentExamRecord.Student.FullName,
                    SubjectName = studentExamRecord.Subject.Name,
                    Score = score,
                    ExamStatus = studentExamRecord.ExamStatus,
                    TotalQuestions = questionsCount,
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while evaluating exam: {ex.Message}");
            }
            return null;

        }
    }
}
