using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SolbegTask2.Models.ModelConfiguration;

public class QuestionModelConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasKey(q => q.QuestionId);
        builder.Property(q => q.QuestionId).ValueGeneratedOnAdd();
        builder.Property(q => q.TimeAdded).HasDefaultValueSql("GETDATE()");
        builder.ToTable("Questions");
    }
}