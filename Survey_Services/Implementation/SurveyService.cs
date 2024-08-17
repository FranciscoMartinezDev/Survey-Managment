using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Survey_DataEntry;
using Survey_DataEntry.Entities;
using Survey_DataTransfer.Dtos;
using Survey_DataTransfer.Mappers;
using Survey_Repository.Implementation;
using Survey_Repository.Interfaces;
using Survey_Services.Exceptions;
using Survey_Services.Interfaces;
using Survey_Services.Validations;

namespace Survey_Services.Implementation
{
    public class SurveyService : ISurveyService
    {
        private SurveyDbContext _context;
        public SurveyService(SurveyDbContext context)
        {
            _context = context;
        }

        #region methods
        /// <summary>
        ///  Metodo llamada de encuestas generales y/o 
        ///  especificas segun el codigo.
        /// </summary>
        /// <param name="code">
        ///  Codigo vinculado a la encuesta.
        /// </param>
        /// <returns>
        ///  Encuestas generales en caso de codigo nulo.
        ///  Encuesta especifica en caso de presencia de codigo.
        /// </returns>
        public async Task<ICollection<SurveyDto>> GetSurveysAsync(string? code)
        {
            using (IUnitOfWork uow = new UnitOfWork(_context))
            {
                ICollection<Survey> surveys = await uow.SurveyRepository.Surveys(code);
                IEntityMapper mapper = new EntityMapper();
                if (!string.IsNullOrEmpty(code))
                {
                    ICollection<SurveyQuestion> questions = await uow.QuestionRepository.Questions(surveys.Select(x => x.IdSurvey));
                    ICollection<QuestionOption> options = await uow.OptionRepository.Options(questions.Select(x => x.IdSurveyQuestion));

                    foreach (var quest in questions)
                    {
                        var linkedOptions = options.Where(x => x.QuestionId == quest.IdSurveyQuestion).ToList();
                        quest.Options = linkedOptions;
                    }

                    foreach (var survey in surveys)
                    {
                        var linkedQuestions = questions.Where(x => x.SurveyId == survey.IdSurvey).ToList();
                        survey.Questions = linkedQuestions;
                    }
                }
                var surveyList = mapper.SurveysListEntityToDto(surveys);
                return surveyList;
            }
        }

        /// <summary>
        ///  Metodo añade encuesta nueva mas preguntas
        ///  mas opciones.
        /// </summary>
        /// <param name="survey">
        ///  Encuesta por añadir.
        /// </param>
        /// <returns>
        ///  Estado de la solicitud.
        /// </returns>
        public async Task SaveSurveyAsync(SurveyDto survey)
        {
            using (IUnitOfWork uow = new UnitOfWork(_context))
            {
                IEntityMapper mapper = new EntityMapper();
                var surveyModel = mapper.SurveyDtoToEntity(survey);
                SurveyValidations.SurveyToSaveValidation(surveyModel);
                await uow.SurveyRepository.AddSurvey(surveyModel);
            }
        }

        /// <summary>
        ///  Metodo actualiza una encuesta.
        /// </summary>
        /// <param name="survey">
        ///  Encuesta por actualizar.
        /// </param>
        /// <returns>
        ///  Estado de la solicitud.
        /// </returns>
        public async Task UpdateSurveyAsync(SurveyDto survey)
        {
            IEntityMapper mapper = new EntityMapper();
            ICollection<QuestionOption> options = new HashSet<QuestionOption>();
            var surveyModel = mapper.SurveyDtoToEntity(survey);
            SurveyValidations.SurveyToSaveValidation(surveyModel);
            using (IUnitOfWork uow = new UnitOfWork(_context))
            {
                var questionWithOptions = surveyModel.Questions.Where(x => x.Options.Count > 0);
                var questionWithoutOptions = surveyModel.Questions.Where(x => x.Options.Count > 0 && !x.QuestionType.Contains("Multiple"));

                foreach (var item in questionWithOptions)
                {
                    foreach (var opt in item.Options)
                    {
                        var option = new QuestionOption
                        {
                            IdOption = opt.IdOption,
                            Option = opt.Option,
                            QuestionId = item.IdSurveyQuestion,
                        };
                        options.Add(option);
                    }
                }

                if (questionWithoutOptions.Count() > 0)
                {
                    var opts = options.Where(x => questionWithoutOptions.Select(q => q.IdSurveyQuestion).Contains(x.QuestionId));
                    await uow.OptionRepository.UnLinkOptions(opts.ToList());
                }

                await uow.SurveyRepository.UpdateSurvey(surveyModel);
                await uow.QuestionRepository.UpdateQuestions(surveyModel.Questions, surveyModel.SurveyCode);
                await uow.OptionRepository.UpdateOptions(options);
            }
        }

        /// <summary>
        ///  Metodo Activa encuesta.
        /// </summary>
        /// <param name="code">
        ///  Codigo vinculado a la encuesta.
        /// </param>
        /// <returns>
        ///  Estado de la solicitud.
        /// </returns>
        public async Task ActivateSurvey(string code)
        {
            using (IUnitOfWork uow = new UnitOfWork(_context))
            {
                var data = await uow.SurveyRepository.Surveys(code);
                var survey = data.First();
                SurveyValidations.SurveyToActivateValidation(survey);
                survey.IsActivated = true;
                survey.DateFrom = DateTime.Now;
                await uow.SurveyRepository.ChangeStateSurvey(survey);
            }
        }

        /// <summary>
        ///  Metodo Desactiva encuesta.
        /// </summary>
        /// <param name="code">
        ///  Codigo vinculado a la encuesta.
        /// </param>
        /// <returns>
        ///  Estado de la solicitud.
        /// </returns>
        public async Task DeactivateSurvey(string code)
        {
            using (IUnitOfWork uow = new UnitOfWork(_context))
            {
                var data = await uow.SurveyRepository.Surveys(code);
                var survey = data.First();
                SurveyValidations.SurveyToDeactivateValidation(survey, async () =>
                {
                    await uow.SurveyRepository.ChangeStateSurvey(survey);
                });
                survey.IsActivated = false;
                survey.DateTo = DateTime.Now;
                await uow.SurveyRepository.ChangeStateSurvey(survey);

            }
        }
        
        #endregion

    }
}
