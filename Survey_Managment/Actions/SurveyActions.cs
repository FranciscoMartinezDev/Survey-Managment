using Survey_Managment.ViewModel;

namespace Survey_Managment.Actions
{
    /// <summary>
    ///  Acciones de iteracion sobre la encuesta.
    /// </summary>
    public class SurveyActions
    {
        public ICollection<QuestionModel> Questions { get; set; }

        public void AddQuestion()
        {
            var newQuestion = new QuestionModel();
            newQuestion.Options = new HashSet<OptionModel>();
            Questions.Add(newQuestion);
        }

        public void QuitQuestion(QuestionModel question)
        {
            Questions = Questions.Where(x => x != question).ToList();
        }

        public SurveyModel HandleSurvey(SurveyModel survey)
        {
            return survey;
        }
    }
}
