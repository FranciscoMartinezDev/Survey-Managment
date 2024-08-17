namespace Survey_Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ISurveyRepository SurveyRepository { get; }
        IQuestionRepository QuestionRepository { get; }
        IOptionRepository OptionRepository { get; }
        new void Dispose();
        void SaveChange();
    }
}
