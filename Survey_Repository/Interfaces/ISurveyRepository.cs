using Survey_DataEntry.Entities;
using Survey_DataTransfer.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey_Repository.Interfaces
{
    public interface ISurveyRepository
    {
        Task<ICollection<Survey>> Surveys(string? code);
        Task AddSurvey(Survey survey);
        Task UpdateSurvey(Survey survey);
        Task ChangeStateSurvey(Survey survey);
    }
}
