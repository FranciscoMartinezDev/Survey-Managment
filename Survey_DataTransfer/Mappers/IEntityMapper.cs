using Survey_DataEntry.Entities;
using Survey_DataTransfer.Dtos;

namespace Survey_DataTransfer.Mappers
{
    public interface IEntityMapper
    {
        ICollection<OptionDto> OptionsListEntityToDto(ICollection<QuestionOption> options);
        ICollection<QuestionDto> QuestionsListEntityToDto(ICollection<SurveyQuestion> questions);
        ICollection<SurveyDto> SurveysListEntityToDto(ICollection<Survey> surveys);
        SurveyDto SurveyEntityToDto(Survey survey);
        ICollection<QuestionOption> OptionDtoListToEntity(IEnumerable<OptionDto> optionDtos);
        ICollection<SurveyQuestion> QuestionDtoListToEntity(IEnumerable<QuestionDto> questionDtos);
        ICollection<Survey> SurveyDtoListToEntity(IEnumerable<SurveyDto> surveyDtos);
        Survey SurveyDtoToEntity(SurveyDto surveyDto);
    }
}
