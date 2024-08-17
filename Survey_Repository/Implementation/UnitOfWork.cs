using Survey_DataEntry;
using Survey_Repository.Interfaces;

namespace Survey_Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {

        private SurveyDbContext _surveyDbContext;
        /// <summary>
        ///  Unidad de trabajo donde se instancian todos los repositorios.
        /// </summary>
        /// <param name="surveyDbContext"></param>
        public UnitOfWork (SurveyDbContext surveyDbContext)
        {
            _surveyDbContext = surveyDbContext;
        }

        private ISurveyRepository _surveyRepository;
        public ISurveyRepository SurveyRepository
        {
            get
            {
                _surveyRepository ??= new SurveyRepository(_surveyDbContext);
                return _surveyRepository;
            }
        }

        private IQuestionRepository _questionRepository;
        public IQuestionRepository QuestionRepository
        {
            get
            {
                _questionRepository ??= new QuestionRepository(_surveyDbContext);
                return _questionRepository;
            }
        }

        private IOptionRepository _optionRepository;
        public IOptionRepository OptionRepository
        {
            get
            {
                _optionRepository ??= new OptionRepository(_surveyDbContext);
                return _optionRepository;
            }
        }

        public void Dispose()
        {
            if (_surveyDbContext == null)
            {
                _surveyDbContext.Dispose();
            }
        }
        public void SaveChange()
        {
           _surveyDbContext.SaveChanges();
        }

    }
}
