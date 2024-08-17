using Survey_DataEntry.Entities;
using Survey_Services.Exceptions;
using System.Linq.Expressions;

namespace Survey_Services.Validations
{
    public static class SurveyValidations
    {

        /// <summary>
        ///  Metodo que valida la encuesta
        ///  antes de guardarla.
        /// </summary>
        /// <param name="survey">
        ///  Encusta por validar.
        /// </param>
        /// <exception cref="SurveyWithoutQuestionsException">
        ///  En caso que la encuesta llegue sin preguntas.
        /// </exception>
        /// <exception cref="QuestionWithoutOptions">
        ///  En caso que las preguntas con opciones lleguen sin opciones.
        /// </exception>
        /// <exception cref="QuestionWithEmptyOptionsException">
        ///  En caso que las preguntas con opciones lleguen con opciones vacias.
        /// </exception>
        /// <exception cref="QuestionEmptyException">
        ///  En caso que las preguntas lleguen vacias.
        /// </exception>
        /// <exception cref="SurveyInvalideDatetimeRangeException">
        ///  En caso que el rango entre las fechas desde y hasta sea invalido.
        /// </exception>
        /// <exception cref="SurveyDatetimeOutOfRangeException">
        ///  En caso que la fecha de fin este fuera del rango permitido.
        /// </exception>
        /// <exception cref="SurveyDataIncompleteException">
        ///  En caso que los datos de la encuesta esten incompletos.
        /// </exception>
        public static void SurveyToSaveValidation(Survey survey)
        {
            if (survey.Questions.Count < 1)
            {
                throw new SurveyWithoutQuestionsException();
            }
            foreach (var question in survey.Questions)
            {
                if (question.Options.Count < 1 && question.QuestionType.Contains("Multiple"))
                {
                    throw new QuestionWithoutOptions();
                }
                foreach (var option in question.Options)
                {
                    if (string.IsNullOrEmpty(option.Option))
                    {
                        throw new QuestionWithEmptyOptionsException();
                    }
                }
                if (string.IsNullOrEmpty(question.Question) || string.IsNullOrEmpty(question.QuestionType))
                {
                    throw new QuestionEmptyException();
                }
            }
            if (!(survey.DateTo >= survey.DateFrom.AddDays(3)))
            {
                throw new SurveyInvalideDatetimeRangeException();
            }
            if (survey.DateTo <= DateTime.Now)
            {
                throw new SurveyDatetimeOutOfRangeException();
            }
            if (string.IsNullOrEmpty(survey.Title) || string.IsNullOrEmpty(survey.Description))
            {
                throw new SurveyDataIncompleteException();
            }
        }

        /// <summary>
        ///  Metodo que valida la activacion de la encuesta.
        /// </summary>
        /// <param name="survey">
        ///  Encuesta por activar.
        /// </param>
        /// <exception cref="SurveyActivatedException">
        ///  En caso que la encuesta ya este en vigencia.
        /// </exception>
        /// <exception cref="SurveyCannotBeActivatedException">
        ///  En caso que sea una encuesta expirada.
        /// </exception>
        public static void SurveyToActivateValidation(Survey survey)
        {
            if (survey.DateTo > DateTime.Now || survey.IsActivated == true)
            {
                throw new SurveyActivatedException();
            }
            if (survey.DateTo < DateTime.Now)
            {
                throw new SurveyCannotBeActivatedException();
            }
        }

        /// <summary>
        ///  Metodo que valida la desactivacion de la encuesta.
        /// </summary>
        /// <param name="survey">
        ///  Encuesta por desactivar.
        /// </param>
        /// <param name="action">
        ///  En caso de ser necesario ejecutar una accion
        ///  como parte de la validacion de la encuesta.
        /// </param>
        /// <exception cref="SurveyNotActiveException">
        ///  En caso que la encuesta no este activada.
        /// </exception>
        /// <exception cref="SurveyNotVigencyException">
        ///  En caso que la encuesta no haya entrado en vigencia aun.
        /// </exception>
        /// <exception cref="SurveyExpiredException">
        ///  En caso que la encuesta este expirada.
        /// </exception>
        public static void SurveyToDeactivateValidation(Survey survey, Action action)
        {
            if (survey.DateFrom < DateTime.Now && survey.DateTo > DateTime.Now && survey.IsActivated == false)
            {
                throw new SurveyNotActiveException();
            }
            if (survey.DateFrom > DateTime.Now && survey.DateTo > DateTime.Now && survey.IsActivated == false)
            {
                throw new SurveyNotVigencyException();
            }
            if (survey.DateTo < DateTime.Now)
            {
                if (survey.IsActivated == true)
                {
                    survey.IsActivated = false;
                    action.Invoke();
                }
                throw new SurveyExpiredException();
            }
        }
    }
}
