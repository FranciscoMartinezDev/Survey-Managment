using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey_DataEntry
{
    public static class DependencyInjection
    {
        /// <summary>
        ///  Metodo extendido de la interface IServiceCollection
        ///  para inyectar las conexiones a la base de datos. 
        /// </summary>
        /// <param name="services"></param>
        public static void AddDataAccess(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            services.AddDbContext<SurveyDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
