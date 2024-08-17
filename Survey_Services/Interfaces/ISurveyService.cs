using Survey_DataTransfer.Dtos;

namespace Survey_Services.Interfaces
{
    public interface ISurveyService
    {
        Task<ICollection<SurveyDto>> GetSurveysAsync(string? code);
        Task SaveSurveyAsync(SurveyDto survey);
        Task UpdateSurveyAsync(SurveyDto survey);
        Task ActivateSurvey(string code);
        Task DeactivateSurvey(string code);
    }
}
