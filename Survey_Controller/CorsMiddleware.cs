namespace Survey_Controller
{
    public static class CorsMiddleware
    {
        /// <summary>
        ///  Metodo extendido de la interface IServiceCollection
        ///  para inyectar las configuraciones de cors. 
        /// </summary>
        /// <param name="services">
        /// </param>
        public static void AddCorsConfiguration(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            string? externalUrl = configuration["ExternalOrigin"];
            if (externalUrl.Contains("localhost"))
            { 
                services.AddCors(options =>
                {
                    options.AddPolicy("AllowExternalOrigin", or =>
                    {
                        or.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost").AllowAnyHeader().AllowAnyMethod();
                    });
                });
            }
            else
            {
                services.AddCors(options =>
                {
                    options.AddPolicy("AllowExternalOrigin", or =>
                    {
                        or.WithOrigins(externalUrl).AllowAnyHeader().AllowAnyMethod();
                    });
                });
            }
        }
    }
}
