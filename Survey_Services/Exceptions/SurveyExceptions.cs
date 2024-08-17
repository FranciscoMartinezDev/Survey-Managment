using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey_Services.Exceptions
{
    /// <summary>
    /// Clase principal control de excepciones.
    /// </summary>
    public class SurveyExceptions : Exception
    {
        public SurveyExceptions(string message) : base(message) { }
    }


    public class SurveyWithoutQuestionsException : SurveyExceptions
    {
        public SurveyWithoutQuestionsException() : base("La encuesta debe tener por lo menos 1 pregunta.") { }
    }

    public class SurveyInvalideDatetimeRangeException : SurveyExceptions
    {
        public SurveyInvalideDatetimeRangeException() : base("La fecha de inicio y la de fin deben tener al menos 3 dias de diferencia.") { }
    }
    public class SurveyDatetimeOutOfRangeException : SurveyExceptions
    {
        public SurveyDatetimeOutOfRangeException() : base("La fecha de fin debe contemplar una fecha posterior a la de hoy.") { }
    }
    public class SurveyDataIncompleteException : SurveyExceptions
    {
        public SurveyDataIncompleteException() : base("Debe completar los valores de titulo y descripción.") { }
    }

    public class SurveyActivatedException : SurveyExceptions
    {
        public SurveyActivatedException() : base("La encuesta ya esta activada.") { }
    }
    public class SurveyCannotBeActivatedException : SurveyExceptions
    {
        public SurveyCannotBeActivatedException() : base("La encuesta ya no se puede activar.") { }
    }
    public class SurveyNotActiveException : SurveyExceptions
    {
        public SurveyNotActiveException() : base("La encuesta no esta activada.") { }
    }
    public class SurveyNotVigencyException : SurveyExceptions
    {
        public SurveyNotVigencyException() : base("La encuesta aun no entra en vigencia.") { }
    }
    public class SurveyExpiredException : SurveyExceptions
    {
        public SurveyExpiredException() : base("La encuesta ya expiro.") { }
    }
}
