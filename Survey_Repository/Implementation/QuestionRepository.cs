using Survey_DataEntry;
using Survey_DataEntry.Entities;
using Survey_DataTransfer.Dtos;
using Survey_Repository.Interfaces;

namespace Survey_Repository.Implementation
{
    public class QuestionRepository : BaseRepository<SurveyQuestion>, IQuestionRepository
    {
        public QuestionRepository(SurveyDbContext context) : base(context) { }

        #region methods

        /// <summary>
        ///  Metodo de llamada de preguntas por encuesta.
        /// </summary>
        /// <param name="surveyId">
        ///  Ids vinculados a la encuesta.
        /// </param>
        /// <returns>
        ///  Listados de Preguntas vinculadas a la encuesta.
        /// </returns>
        public async Task<ICollection<SurveyQuestion>> Questions(IEnumerable<int> surveyId)
        {
            var dbQuestions = await GetOneOrMoreAsync(x => surveyId.Contains(x.SurveyId));
            return dbQuestions.ToList();
        }

        /// <summary>
        ///  Metodo actualiza listado de preguntas por encuesta.
        /// </summary>
        /// <param name="questions">
        ///  Listado de preguntas por actualizar.
        /// </param>
        /// <param name="code">
        ///  Codigo vinculado a la encuesta.
        /// </param>
        /// <returns>
        ///  Estado de la solicitud.
        /// </returns>
        public async Task UpdateQuestions(ICollection<SurveyQuestion> questions, string code)
        {
            var dbQuestions = await GetOneOrMoreAsync(x => x.Survey.SurveyCode == code);

            var newQuestions = questions.Where(x => x.IdSurveyQuestion == 0).ToList();
            var existingQuestions = dbQuestions.Where(x => 
                                                        questions.Select(q => q.IdSurveyQuestion)
                                                                    .Contains(x.IdSurveyQuestion));
            
            var nonExistentQuestions = dbQuestions.Where(x => !questions.Select(q => q.IdSurveyQuestion)
                                                                        .Contains(x.IdSurveyQuestion))
                                                        .Where(x => x.IdSurveyQuestion != 0);

            if (existingQuestions.Count() > 0)
            {
                foreach (var question in existingQuestions)
                {
                    var currentQuestion = questions.Where(x => x.IdSurveyQuestion == question.IdSurveyQuestion).First();
                    question.Question = currentQuestion.Question;
                    question.QuestionType = currentQuestion.QuestionType;
                }
                await SaveChangeAsync();
            }
            if (nonExistentQuestions.Count() > 0)
            {
                await MultiDelete(nonExistentQuestions.ToList());
                await SaveChangeAsync();
            }
            if (newQuestions.Count > 0)
            {
                newQuestions.ForEach(x => x.SurveyId =  existingQuestions.First().SurveyId);
                await MultiAddAsync(newQuestions);
                await SaveChangeAsync();
            }
        }

        #endregion
    }
}
