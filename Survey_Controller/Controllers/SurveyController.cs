using Microsoft.AspNetCore.Mvc;
using Survey_DataEntry;
using Survey_DataTransfer.Dtos;
using Survey_Services.Implementation;
using Survey_Services.Interfaces;

namespace Survey_Controller.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class SurveyController : Controller
    {
        private ISurveyService _surveyService;
        public SurveyController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        /// <summary>
        ///  Metodo general de todas las encuestas.
        /// </summary>
        /// <returns>
        ///  Todas las encuestas de la base de datos.
        /// </returns>
        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            try
            {
                var surveys = await _surveyService.GetSurveysAsync(null);
                return Json(surveys);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        ///  Metodo que busca una encuesta en particular.
        /// </summary>
        /// <param name="code">
        ///  Codigo de encuesta como parametro de busqueda.
        /// </param>
        /// <returns>
        ///  Retorna encuesta vinculada al codigo.
        /// </returns>
        [HttpGet("{code}")]
        public async Task<IActionResult> SurveyByCode(string code)
        {
            try
            {
                var surveys = await _surveyService.GetSurveysAsync(code);
                return Json(surveys.First());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        ///  Metodo que añade una encuesta nueva.
        /// </summary>
        /// <param name="survey">
        ///  Encuesta dentro del cuerpo de solititud.
        /// </param>
        /// <returns>
        ///  Retorna el estado de la solicitud.
        /// </returns>
        [HttpPost("New-Survey")]
        public async Task<IActionResult> AddSurvey([FromBody] SurveyDto survey)
        {
            try
            {
                string message;
                message = $"Se Guardo una nueva encuesta bajo el numero {survey.SurveyCode}.";
                survey.SurveyCode = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                survey.IsActivated = false;
                survey.CreatedDate = DateTime.Now;

                if (survey.DateFrom <= DateTime.Now)
                {
                    survey.IsActivated = true;
                    message = $"Se Guardo y se activo una nueva encuesta bajo el numero {survey.SurveyCode}.";
                }
                await _surveyService.SaveSurveyAsync(survey);
                return Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        ///  Metodo que actualiza una encuesta.
        /// </summary>
        /// <param name="survey">
        ///  Encuesta dentro del cuerpo de solititud.
        /// </param>
        /// <returns>
        ///  Retorna el estado de la solicitud.
        /// </returns>
        [HttpPost("Update-Survey")]
        public async Task<IActionResult> UpdateSurvey([FromBody] SurveyDto survey)
        {
            try
            {
                await _surveyService.UpdateSurveyAsync(survey);
                return Ok("¡Bien hecho! La encuesta se actualizo correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        ///  Metodo que pone en vigencia la encuesta.
        /// </summary>
        /// <param name="code">
        ///  Codigo de la encuesta dentro del cuerpo de solititud.
        /// </param>
        /// <returns>
        ///  Retorna el estado de la solicitud.
        /// </returns>
        [HttpPost("Activate")]
        public async Task<IActionResult> Activate([FromBody] string code)
        {
            try
            {
                await _surveyService.ActivateSurvey(code);
                return Ok("¡La encuesta entro en vigencia exitosamente!.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        ///  Metodo que quita de vigencia la encuesta.
        /// </summary>
        /// <param name="code">
        ///  Codigo de la encuesta dentro del cuerpo de solititud.
        /// </param>
        /// <returns>
        ///  Retorna el estado de la solicitud.
        /// </returns>
        [HttpPost("Deactivate")]
        public async Task<IActionResult> Desactivate([FromBody] string code)
        {
            try
            {
                await _surveyService.DeactivateSurvey(code);
                return Ok("¡La encuesta salio de vigencia exitosamente!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
