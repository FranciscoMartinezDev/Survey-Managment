namespace Survey_Managment
{
    public static class DependencyInjection
    {
        public static string BaseUrl { get; private set; }

        /// <summary>
        ///  Toma la url base almacenada en el appsettings.
        /// </summary>
        /// <param name="configuration"></param>
        public static void InicializeBaseUrl(this IConfiguration configuration)
        {
            string url = configuration["ExternalUrl"];
            BaseUrl = url;
        }

    }
}
