using Survey_DataEntry.Entities;
using Survey_DataTransfer.Dtos;

namespace Survey_DataTransfer.Mappers
{
    public class EntityMapper : IEntityMapper
    {

        #region EntityToDto

        /// <summary>
        ///  Metodo convierte lista de entidades a lista dto.
        /// </summary>
        /// <param name="options">
        ///  Lista de entidades.
        /// </param>
        /// <returns>
        ///  Lista Dto.
        /// </returns>
        public ICollection<OptionDto> OptionsListEntityToDto(ICollection<QuestionOption> options)
        {
            ICollection<OptionDto> optionDtos = new HashSet<OptionDto>();

            foreach (var option in options)
            {
                var optionDto = new OptionDto
                {
                    IdOption = option.IdOption,
                    Option = option.Option,
                    IsSelected = option.IsSelected,
                    QuestionId = option.QuestionId,
                };
                optionDtos.Add(optionDto);
            }
            return optionDtos;
        }

        /// <summary>
        ///  Metodo convierte lista de entidades a lista dto.
        /// </summary>
        /// <param name="questions">
        ///  Lista de entidades.
        /// </param>
        /// <returns>
        ///  Lista Dto.
        /// </returns>
        public ICollection<QuestionDto> QuestionsListEntityToDto(ICollection<SurveyQuestion> questions)
        {
            ICollection<QuestionDto> questionDtos = new HashSet<QuestionDto>();

            foreach (var question in questions)
            {
                ICollection<OptionDto>? optionDtos = question.Options != null ?
                                                     OptionsListEntityToDto(question.Options) :
                                                     null;
                var questionDto = new QuestionDto
                {
                    Question = question.Question,
                    Type = question.QuestionType,
                    IdSurveyQuestion = question.IdSurveyQuestion,
                    Options = optionDtos
                };
                questionDtos.Add(questionDto);
            }
            return questionDtos;
        }

        /// <summary>
        ///  Metodo convierte lista de entidades a lista dto.
        /// </summary>
        /// <param name="Surveys">
        ///  Lista de entidades.
        /// </param>
        /// <returns>
        ///  Lista Dto.
        /// </returns>
        public ICollection<SurveyDto> SurveysListEntityToDto(ICollection<Survey> surveys)
        {
            ICollection<SurveyDto> surveyDtos = new HashSet<SurveyDto>();
            foreach (var survey in surveys)
            {
                ICollection<QuestionDto>? questions = survey.Questions != null ?
                                                      QuestionsListEntityToDto(survey.Questions) :
                                                      null;
                var surveyDto = new SurveyDto
                {
                    Title = survey.Title,
                    Description = survey.Description,
                    CreatedDate = survey.CreatedDate,
                    DateFrom = survey.DateFrom,
                    DateTo = survey.DateTo,
                    IsActivated = survey.IsActivated,
                    SurveyCode = survey.SurveyCode,
                    Questions = questions
                };
                surveyDtos.Add(surveyDto);
            }

            return surveyDtos;
        }

        /// <summary>
        ///  Metodo convierte entidad a dto.
        /// </summary>
        /// <param name="survey">
        ///  Entidad.
        /// </param>
        /// <returns>
        ///  Dto.
        /// </returns>
        public SurveyDto SurveyEntityToDto(Survey survey)
        {
            ICollection<QuestionDto>? questions = survey.Questions.Count > 0 ?
                                                  QuestionsListEntityToDto(survey.Questions) :
                                                  null;
            var surveyDto = new SurveyDto
            {
                Title = survey.Title,
                Description = survey.Description,
                CreatedDate = survey.CreatedDate,
                DateFrom = survey.DateFrom,
                DateTo = survey.DateTo,
                IsActivated = survey.IsActivated,
                SurveyCode = survey.SurveyCode,
                Questions = questions
            };
            return surveyDto;
        }

        #endregion

        #region DtoToEntity

        /// <summary>
        ///  Metodo contierte lista de dto a lista de entidades. 
        /// </summary>
        /// <param name="optionDtos">
        ///  Lista de Dtos.
        /// </param>
        /// <returns>
        ///  Lista de Entidades.
        /// </returns>
        public ICollection<QuestionOption> OptionDtoListToEntity(IEnumerable<OptionDto> optionDtos)
        {
            ICollection<QuestionOption> options = new HashSet<QuestionOption>();
            foreach (var option in optionDtos)
            {
                var item = new QuestionOption
                {
                    IdOption = option.IdOption,
                    Option = option.Option,
                    IsSelected = option.IsSelected,
                    QuestionId = option.QuestionId,
                };
                options.Add(item);
            }
            return options;
        }

        /// <summary>
        ///  Metodo contierte lista de dto a lista de entidades. 
        /// </summary>
        /// <param name="questionDtos">
        ///  Lista de Dtos.
        /// </param>
        /// <returns>
        ///  Lista de Entidades.
        /// </returns>
        public ICollection<SurveyQuestion> QuestionDtoListToEntity(IEnumerable<QuestionDto> questionDtos)
        {
            ICollection<SurveyQuestion> questions = new HashSet<SurveyQuestion>();

            foreach (var question in questionDtos)
            {
                ICollection<QuestionOption> options = new HashSet<QuestionOption>();
                if (question.Options != null)
                {
                    options = OptionDtoListToEntity(question.Options);

                }

                var item = new SurveyQuestion
                {
                    Question = question.Question,
                    QuestionType = question.Type,
                    IdSurveyQuestion = question.IdSurveyQuestion,
                    Options = options
                };
                questions.Add(item);
            }
            return questions;
        }

        /// <summary>
        ///  Metodo contierte lista de dto a lista de entidades. 
        /// </summary>
        /// <param name="surveyDtos">
        ///  Lista de Dtos.
        /// </param>
        /// <returns>
        ///  Lista de Entidades.
        /// </returns>
        public ICollection<Survey> SurveyDtoListToEntity(IEnumerable<SurveyDto> surveyDtos)
        {
            ICollection<Survey> surveys = new HashSet<Survey>();
            foreach (var survey in surveyDtos)
            {
                ICollection<SurveyQuestion>? questions = survey.Questions.Count > 0 ?
                                                         QuestionDtoListToEntity(survey.Questions) :
                                                         null;
                var item = new Survey
                {
                    Title = survey.Title,
                    Description = survey.Description,
                    CreatedDate = survey.CreatedDate,
                    DateFrom = survey.DateFrom,
                    DateTo = survey.DateTo,
                    IsActivated = survey.IsActivated,
                    SurveyCode = survey.SurveyCode,
                    Questions = questions,
                };
                surveys.Add(item);
            }
            return surveys;
        }

        /// <summary>
        ///  Metodo contierte dto a entidad. 
        /// </summary>
        /// <param name="surveyDto">
        ///  Dto.
        /// </param>
        /// <returns>
        ///  Entidad.
        /// </returns>
        public Survey SurveyDtoToEntity(SurveyDto surveyDto)
        {
            ICollection<SurveyQuestion>? questions = surveyDto.Questions.Count > 0 ?
                                                    QuestionDtoListToEntity(surveyDto.Questions) :
                                                    null;
            var entity = new Survey
            {
                Title = surveyDto.Title,
                Description = surveyDto.Description,
                CreatedDate = surveyDto.CreatedDate,
                DateFrom = surveyDto.DateFrom,
                DateTo = surveyDto.DateTo,
                IsActivated = surveyDto.IsActivated,
                SurveyCode = surveyDto.SurveyCode,
                Questions = questions,
            };
            return entity;
        }

        #endregion

    }
}
