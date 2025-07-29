using EvaluationService.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EvaluationService.Infrastructure.Data
{
    public class EvaluationDbContext(DbContextOptions<EvaluationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionChoice> QuestionChoices { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }
        public DbSet<StudentExam> StudentExams { get; set; }

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

            modelBuilder.Entity<Subject>().HasQueryFilter(s => s.DeletedAt == null);
            modelBuilder.Entity<Question>().HasQueryFilter(q => q.DeletedAt == null);
            modelBuilder.Entity<QuestionChoice>().HasQueryFilter(q => q.Question.DeletedAt == null);

            modelBuilder.Entity<StudentExam>().HasQueryFilter(s => s.DeletedAt == null);
            

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EvaluationDbContext).Assembly);

        }
    }
}
