using Survey_DataEntry.Entities;
using Survey_DataTransfer.Dtos;

namespace Survey_Repository.Interfaces
{
    public interface IQuestionRepository
    {
        Task<ICollection<SurveyQuestion>> Questions(IEnumerable<int> surveyId);
        Task UpdateQuestions(ICollection<SurveyQuestion> questions, string code);
    }
}
