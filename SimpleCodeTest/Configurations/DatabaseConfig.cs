namespace CodeTest.Configurations
{
    public class DatabaseConfig
    {
        public DatabaseConfig(DatabaseProvider databaseProvider,
            string host, string hostPrefix,
            string port, string username,
            string password, string database)
        {
            DatabaseProvider = databaseProvider;
            Host = host;
            HostPrefix = hostPrefix;
            Port = port;
            Username = username;
            Password = password;
            Database = database;
        }

        public DatabaseProvider DatabaseProvider { get; set; }

        public string Host { get; set; }

        public string HostPrefix { get; set; } // For MongoDB Only

        public string Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Database { get; set; }
    }

    public enum DatabaseProvider
    {
        PostgreSQL,
        MongoDB,
        SQLite,
        SQLServer
    }
}
