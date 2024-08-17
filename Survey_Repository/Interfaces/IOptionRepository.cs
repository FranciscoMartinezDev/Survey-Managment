using Survey_DataEntry.Entities;
using Survey_DataTransfer.Dtos;

namespace Survey_Repository.Interfaces
{
    public interface IOptionRepository
    {
        Task<ICollection<QuestionOption>> Options(IEnumerable<int> questionIds);
        Task UpdateOptions(ICollection<QuestionOption> questions);
        Task UnLinkOptions(ICollection<QuestionOption> options);
    }
}
