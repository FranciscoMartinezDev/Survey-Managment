using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Survey_Services.Implementation;
using Survey_Services.Interfaces;

namespace Survey_Services
{
    /// <summary>
    ///  Inyeccion de dependencia de los servicios.
    /// </summary>
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services)
        { 
            services.AddScoped<ISurveyService, SurveyService>();
        }
    }
}
