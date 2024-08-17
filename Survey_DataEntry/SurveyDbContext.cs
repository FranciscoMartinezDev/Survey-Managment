using Microsoft.EntityFrameworkCore;
using Survey_DataEntry.Entities;

namespace Survey_DataEntry
{
    public class SurveyDbContext : DbContext
    {
        /// <summary>
        ///  Configuracion de contexto para acceso de datos.
        /// </summary>
        /// <param name="context"></param>
        public SurveyDbContext(DbContextOptions<SurveyDbContext> context) : base(context) { }

        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<QuestionOption> QuestionOptions { get; set; }

        /// <summary>
        ///  Creacion y configuracion de entidades.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Survey>().ToTable("Survey");
            modelBuilder.Entity<SurveyQuestion>().ToTable("SurveyQuestion");
            modelBuilder.Entity<QuestionOption>().ToTable("QuestionOption");
        }
    }
}