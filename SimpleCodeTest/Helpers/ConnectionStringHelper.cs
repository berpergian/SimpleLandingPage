using CodeTest.Configurations;

namespace CodeTest.Helpers
{
    public static class ConnectionStringHelper
    {
        public static string BuildPostgres(DatabaseConfig config)
        {
            return $"Host={config.Host};Port={config.Port};Database={config.Database};Username={config.Username};Password={config.Password}";
        }

        public static string BuildSqlite(DatabaseConfig config)
        {
            // SQLite uses file-based DB, Database field is used for file name
            return $"Data Source={config.Database}.db";
        }

        public static string BuildMongo(DatabaseConfig config)
        {
            return $"mongodb://{config.Username}:{config.Password}@{config.HostPrefix}{config.Host}:{config.Port}";
        }
    }
}
