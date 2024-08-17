using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Extensions;
using Survey_DataTransfer.Dtos;
using Survey_Managment.ViewModel;
using System.Collections;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Survey_Managment.Services
{
    public class SurveyClient
    {
        private NavigationManager _navigationManager;
        private IDialogService _dialogService;
        private ISnackbar _snackBar;
        public SurveyClient(NavigationManager navigationManager, IDialogService dialogService, ISnackbar snackBar)
        {
            _navigationManager = navigationManager;
            _dialogService = dialogService;
            _snackBar = snackBar;
        }

        #region methods
        /// <summary>
        ///  Metodo de llamada general para todas las encuestas.
        /// </summary>
        /// <returns>
        ///  Lista de Encuestas.
        /// </returns>
        /// <exception cref="Exception">
        ///  Excepcion controlada por el Servidor.
        /// </exception>
        public async Task<ICollection<SurveyModel>> AllSurveys()
        {
            try
            {
                ICollection<SurveyModel> surveyList = new List<SurveyModel>();
                using (var client = new HttpClient())
                {
                    var url = DependencyInjection.BaseUrl + "Survey/List";
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        surveyList = await response.Content.ReadFromJsonAsync<List<SurveyModel>>();
                    }
                }
                return surveyList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///  Metodo llamada de encuesta por codigo.
        /// </summary>
        /// <param name="code">
        ///  Codigo vinculado a la encuesta.
        /// </param>
        /// <returns>
        ///  Datos de la encuesta seleccionada.
        /// </returns>
        /// <exception cref="Exception">
        ///  Excepcion controlada por el Servidor. 
        /// </exception>
        public async Task<SurveyModel> SurveyByCode(string code)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = DependencyInjection.BaseUrl + $"Survey/{code}";
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    var responseString = await response.Content.ReadAsStringAsync();
                    var deserialize = JsonSerializer.Deserialize<SurveyDto>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    ICollection<QuestionModel> questions = new List<QuestionModel>();

                    foreach (var quest in deserialize.Questions)
                    {
                        ICollection<OptionModel> options = new HashSet<OptionModel>();
                        if (quest.Options != null)
                        {
                            foreach (var opt in quest.Options)
                            {
                                var option = new OptionModel
                                {
                                    OptionId = opt.IdOption,
                                    Option = opt.Option,
                                };
                                options.Add(option);
                            }
                        }

                        var question = new QuestionModel
                        {
                            Question = quest.Question,
                            QuestionId = quest.IdSurveyQuestion,
                            Type = quest.Type,
                            Options = options
                        };
                        questions.Add(question);
                    }

                    var survey = new SurveyModel
                    {
                        Title = deserialize.Title,
                        Description = deserialize.Description,
                        CreatedDate = deserialize.CreatedDate,
                        DateFrom = deserialize.DateFrom,
                        DateTo = deserialize.DateTo,
                        Questions = questions,
                    };

                    return survey;
                }
            }
            catch (Exception ex)
            {
                _snackBar.Add("¡Ups!, Algo salio mal.", Severity.Error);
                throw new Exception($"{ex.Message}");
            }
        }

        /// <summary>
        ///  Metodo que guarda una encuesta en la base de datos.
        /// </summary>
        /// <param name="survey">
        ///  Encuesta a guardar.
        /// </param>
        /// <returns>
        ///  Estado de la solicitud.
        /// </returns>
        /// <exception cref="Exception">
        ///  Excepcion controlada por el Servidor. 
        /// </exception>
        public async Task SaveSurvey(SurveyModel survey)
        {
            try
            {
                ICollection<QuestionDto> questionList = new List<QuestionDto>();

                foreach (var quest in survey.Questions)
                {
                    ICollection<OptionDto> optionList = new List<OptionDto>();
                    var question = new QuestionDto()
                    {
                        Question = quest.Question,
                        Type = quest.Type,
                    };
                    foreach (var opt in quest.Options)
                    {
                        var option = new OptionDto
                        {
                            IdOption = opt.OptionId,
                            Option = opt.Option
                        };
                        optionList.Add(option);
                    }
                    question.Options = optionList;
                    questionList.Add(question);
                }

                var newSurvey = new SurveyDto()
                {
                    Title = survey.Title,
                    Description = survey.Description,
                    DateFrom = survey.DateFrom,
                    DateTo = survey.DateTo,
                    Questions = questionList
                };

                using (var client = new HttpClient())
                {
                    var url = $"{DependencyInjection.BaseUrl}Survey/New-Survey";
                    var response = await client.PostAsJsonAsync(url, newSurvey);
                    var message = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _snackBar.Add(message, Severity.Success);
                        await Task.Delay(1000);
                        _navigationManager.NavigateTo("/");
                    }
                    else
                    {
                        _snackBar.Add(message, Severity.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                _snackBar.Add("¡Ups! Algo Salio mal.", Severity.Error);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///  Metodo que actualiza la encuesta.
        /// </summary>
        /// <param name="survey">
        ///  Encuesta a actualizar
        /// </param>
        /// <param name="code">
        ///  Codigo vinculado a la encuesta.
        /// </param>
        /// <returns>
        ///  Estado de la solicitud.
        /// </returns>
        /// <exception cref="Exception">
        ///  Excepcion controlada por el Servidor.
        /// </exception>
        public async Task UpdateSurvey(SurveyModel survey, string code)
        {
            try
            {
                ICollection<QuestionDto> questions = new List<QuestionDto>();
                foreach (var ques in survey.Questions)
                {
                    ICollection<OptionDto> options = new List<OptionDto>();
                    foreach (var opt in ques.Options)
                    {
                        var option = new OptionDto
                        {
                            IdOption = opt.OptionId,
                            Option = opt.Option,
                        };
                        options.Add(option);
                    }

                    var question = new QuestionDto
                    {
                        IdSurveyQuestion = ques.QuestionId,
                        Question = ques.Question,
                        Type = ques.Type,
                        Options = options
                    };
                    questions.Add(question);
                }

                SurveyDto surveyData = new SurveyDto
                {
                    Title = survey.Title,
                    Description = survey.Description,
                    DateFrom = survey.DateFrom,
                    DateTo = survey.DateTo,
                    SurveyCode = code,
                    Questions = questions,
                };

                using (var client = new HttpClient())
                {
                    var url = $"{DependencyInjection.BaseUrl}Survey/Update-Survey";
                    var response = await client.PostAsJsonAsync(url, surveyData);
                    var message = await response.Content.ReadAsStringAsync();
                    
                    if (response.IsSuccessStatusCode)
                    {
                        _snackBar.Add(message, Severity.Success);
                        await Task.Delay(1000);
                        _navigationManager.NavigateTo("/");
                    }
                    else
                    {
                        _snackBar.Add(message, Severity.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                _snackBar.Add("¡Ups! Algo salio mal", Severity.Error);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///  Metodo que pone en vigencia la encuesta.
        /// </summary>
        /// <param name="code">
        ///  Codigo vinculado a la encuesta.
        /// </param>
        /// <returns>
        ///  Estado de la solicitud.
        /// </returns>
        /// <exception cref="Exception">
        ///  Excepcion controlada por el Servidor.
        /// </exception>
        public async Task Active(string code)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = DependencyInjection.BaseUrl + $"Survey/Activate";
                    var response = await client.PostAsJsonAsync(url, code);
                    var message = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _snackBar.Add(message, Severity.Success);
                    }
                    else
                    {
                        _snackBar.Add(message, Severity.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                _snackBar.Add("¡Ups! Algo salio mal", Severity.Error);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///  Metodo que quita de vigencia la encuesta.
        /// </summary>
        /// <param name="code">
        ///  Codigo vinculado a la encuesta.
        /// </param>
        /// <returns>
        ///  Estado de la solicitud.
        /// </returns>
        /// <exception cref="Exception">
        ///  Excepcion controlada por el Servidor.
        /// </exception>
        public async Task Deactive(string code)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = DependencyInjection.BaseUrl + $"Survey/Deactivate";
                    var response = await client.PostAsJsonAsync(url, code);
                    var message = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _snackBar.Add(message, Severity.Success);
                    }
                    else
                    {
                        _snackBar.Add(message, Severity.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                _snackBar.Add("¡Ups! Algo salio mal", Severity.Error);
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
