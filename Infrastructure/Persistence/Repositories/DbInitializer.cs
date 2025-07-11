using Domain.Contracts;
using Domain.Models;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class DbInitializer(ExaminationDbContext context) : IDbInitializer
    {
        public async Task InitializeAsync()
        {
            try
            {

                if (!context.Set<Subject>().Any())
                {
                    var subjects = new List<Subject>() {

                        new Subject{Name="OOP",

                        ExamConfiguration= new ExamConfiguration{
                        HardPercentage=50,
                        LowPercentage=20,
                        NormalPercentage=30,
                        QuestionNumbers=10},
                        Questions=new List<Question>()
                        {
                            new Question
                            {
                                Text="Why Khaled Abdelzaher?",
                                QuestionLevel=Domain.Enums.DifficultyLevel.High,
                                Choices=new List<QuestionChoice>()
                                {
                                    new QuestionChoice{Text="Khaled",isCorrect=true},
                                    new QuestionChoice{Text="Ahmed",isCorrect=false},
                                    new QuestionChoice{Text="Seif",isCorrect=false},
                                    new QuestionChoice{Text="Kareem",isCorrect=false},
                                }
                            },
                            new Question
                            {
                                Text="Why Kareem?",
                                QuestionLevel=Domain.Enums.DifficultyLevel.Normal,
                                Choices=new List<QuestionChoice>()
                                {
                                    new QuestionChoice{Text="Atos",isCorrect=true},
                                    new QuestionChoice{Text="ITI",isCorrect=false},
                                    new QuestionChoice{Text="Ejada",isCorrect=false},
                                    new QuestionChoice{Text="Arbu",isCorrect=false},
                                }
                            },
                            new Question
                            {
                                Text="Why .NET?",
                                QuestionLevel=Domain.Enums.DifficultyLevel.Low,
                                Choices=new List<QuestionChoice>()
                                {
                                    new QuestionChoice{Text="CORE",isCorrect=false},
                                    new QuestionChoice{Text="EF",isCorrect=false},
                                    new QuestionChoice{Text="SQLServer",isCorrect=true},
                                    new QuestionChoice{Text="SignalR",isCorrect=false},
                                }
                            },
                        },
                        
                        },
                        new Subject
                        {
                            Name="Arabic",
                            ExamConfiguration= new ExamConfiguration
                            {
                                HardPercentage=10,
                                LowPercentage=70,
                                NormalPercentage=20,
                                QuestionNumbers=20
                            }
                        }
                        
                    };
                    await context.Set<Subject>().AddRangeAsync(subjects);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
