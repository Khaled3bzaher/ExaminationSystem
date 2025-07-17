using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data
{
    public class ExaminationDbContext(DbContextOptions<ExaminationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<ExamConfiguration> ExamConfigurations { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionChoice> QuestionChoices { get; set; }
        public DbSet<StudentExam> StudentExams { get; set; }
        public DbSet<ExamQuestion> ExamQuestions { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Identity Configurations
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

            //Soft Delete Configurations
            modelBuilder.Entity<Subject>().HasQueryFilter(s => s.DeletedAt == null);
            modelBuilder.Entity<Question>().HasQueryFilter(s => s.DeletedAt == null);
            modelBuilder.Entity<QuestionChoice>().HasQueryFilter(s => s.Question.DeletedAt == null);
            modelBuilder.Entity<ExamConfiguration>().HasQueryFilter(s => s.Subject.DeletedAt == null);
            modelBuilder.Entity<StudentExam>().HasQueryFilter(s => s.DeletedAt == null);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);
        }
    }
}
