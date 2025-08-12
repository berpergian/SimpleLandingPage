namespace CodeTest.Configurations.Startup
{
    public static class AppsettingStartup
    {
        public static AppsettingConfig AddAppsetting(this IServiceCollection services, IConfiguration configuration)
        {
            var appsettingConfig = new AppsettingConfig(configuration);
            services.AddSingleton(appsettingConfig);

            return appsettingConfig;
        }
    }
}
