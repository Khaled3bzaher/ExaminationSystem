using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    internal class ExamQuestionConfiguration : IEntityTypeConfiguration<ExamQuestion>
    {
        public void Configure(EntityTypeBuilder<ExamQuestion> builder)
        {
            builder.
                HasOne(eq=>eq.SelectedChoice)
                .WithMany()
                .HasForeignKey(eq=>eq.SelectedChoiceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
