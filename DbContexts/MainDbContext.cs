using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SolbegTask2.Models;
using SolbegTask2.Models.ModelConfiguration;

namespace SolbegTask2.DbContexts;

public class MainDbContext : DbContext
{
    public DbSet<Question> Questions { get; set; }
    
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options){}
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new QuestionModelConfiguration());
    }
}