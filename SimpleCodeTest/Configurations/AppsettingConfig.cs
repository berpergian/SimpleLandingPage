namespace CodeTest.Configurations
{
    public class AppsettingConfig
    {
        private readonly IConfiguration _configuration;

        private const string _databaseConfigKey = "Database";

        private DatabaseConfig _databaseConfig;

        public AppsettingConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DatabaseConfig Database
        {
            get
            {
                _databaseConfig ??= _configuration.GetSection(_databaseConfigKey).Get<DatabaseConfig>() ??
                        throw new InvalidOperationException($"Cannot get {nameof(DatabaseConfig)} in appsetting");

                return _databaseConfig;
            }
        }
    }
}
