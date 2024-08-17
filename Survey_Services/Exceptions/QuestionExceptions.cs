using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey_Services.Exceptions
{
    /// <summary>
    ///  Clase principal control de excepciones.
    /// </summary>
    public class QuestionExceptions : Exception
    {
        public QuestionExceptions(string message) : base(message) { }
    }
    public class QuestionWithoutOptions : QuestionExceptions
    {
        public QuestionWithoutOptions() : base("La preguntas de opción multiple y respuesta multiple deben tener por lo menos una opción.") { }
    }
    public class QuestionEmptyException : QuestionExceptions
    {
        public QuestionEmptyException() : base("La pregunta debe estar completa y no vacia.") { }
    }
    public class QuestionWithEmptyOptionsException : QuestionExceptions
    {
        public QuestionWithEmptyOptionsException() : base("La preguntas de opción multiple y respuesta multiple NO deben tener opciones vacias") { }
    }
}
