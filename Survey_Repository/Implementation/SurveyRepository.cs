using Survey_DataEntry;
using Survey_DataEntry.Entities;
using Survey_Repository.Interfaces;

namespace Survey_Repository.Implementation
{
    public class SurveyRepository : BaseRepository<Survey>, ISurveyRepository
    {
        public SurveyRepository(SurveyDbContext context) : base(context) { }

        #region methods

        /// <summary>
        ///  Metodo llamada general de encuestas o
        ///  busqueda por codigo.
        /// </summary>
        /// <param name="code">
        ///  Codigo vinculado a la encuesta, 
        ///  si el valor es nulo toma todas las encuestas.
        /// </param>
        /// <returns>
        ///  Listado general de encuestas.
        /// </returns>
        public async Task<ICollection<Survey>> Surveys(string? code)
        {
            var dbSurveys = string.IsNullOrEmpty(code) ? await GetAllAsync() : await GetOneOrMoreAsync(x => x.SurveyCode == code);
            return dbSurveys;
        }

        /// <summary>
        ///  Metodo de insercion de nueva encuesta.
        /// </summary>
        /// <param name="survey">
        ///  Encuesta por añadir
        /// </param>
        /// <returns>
        ///  Estado de la solicitud.
        /// </returns>
        public async Task AddSurvey(Survey survey)
        {
            await AddAsync(survey);
            await SaveChangeAsync();
        }

        /// <summary>
        ///  Metodo de actualizacion de encuesta.
        /// </summary>
        /// <param name="survey">
        ///  Encuesta por actualizar.
        /// </param>
        /// <returns>
        ///  Estado de la solicitud.
        /// </returns>
        public async Task UpdateSurvey(Survey survey)
        {
            var dbSurvey = await GetOneOrMoreAsync(x => x.SurveyCode == survey.SurveyCode);
            var surveyData = dbSurvey.First();
            surveyData.Title = survey.Title;
            surveyData.Description = survey.Description;
            surveyData.DateFrom = survey.DateFrom;
            surveyData.DateTo = survey.DateTo;
            await SaveChangeAsync();
        }

        /// <summary>
        ///  Metodo para activar y desactivar encuesta.
        /// </summary>
        /// <param name="survey">
        ///  Encuesta por actualizar.
        /// </param>
        /// <returns>
        ///  Estado de la solicitud
        /// </returns>
        public async Task ChangeStateSurvey(Survey survey)
        {
            var dbSurvey = await GetOneOrMoreAsync(x => x.SurveyCode == survey.SurveyCode);
            dbSurvey.First().IsActivated = survey.IsActivated;
            dbSurvey.First().DateFrom = survey?.DateFrom != null ?
                                        survey.DateFrom :
                                        dbSurvey.First().DateFrom;
            dbSurvey.First().DateTo = survey?.DateTo != null ?
                                      survey.DateTo :
                                      dbSurvey.First().DateTo;
            await SaveChangeAsync();
        }

        #endregion
    }
}
