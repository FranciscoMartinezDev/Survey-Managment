using Microsoft.Extensions.DependencyInjection;
using Survey_Repository.Implementation;
using Survey_Repository.Interfaces;

namespace Survey_Repository
{
    public static class DependencyInyection
    {
        /// <summary>
        ///  Inyeccion de las dependencias de servicios.
        /// </summary>
        /// <param name="services"></param>
        public static void AddAplicationRepositories(this IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISurveyRepository, SurveyRepository>();
        }
    }
}
