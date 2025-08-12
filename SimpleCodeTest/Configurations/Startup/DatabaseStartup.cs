using CodeTest.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CodeTest.Configurations.Startup
{
    public static class DatabaseStartup
    {
        public static void AddDatabase(this IServiceCollection services, AppsettingConfig appsetting)
        {
            services.AddDbContext<DatabaseContext>(options =>
                    options.UseNpgsql(ConnectionStringHelper.BuildPostgres(appsetting.Database),
                    x => x.MigrationsHistoryTable("__EFMigrationsHistory", "postgresTest")));
            //switch (appsetting.Database.DatabaseProvider)
            //{
            //    case DatabaseProvider.PostgreSQL:
            //        services.AddDbContext<DatabaseContext>(options =>
            //            options.usenq(ConnectionStringHelper.BuildPostgres(appsetting.Database),
            //            x => x.MigrationsHistoryTable("__EFMigrationsHistory", "postgres")));
            //        break;

            //    case DatabaseProvider.SQLite:
            //        services.AddDbContext<DatabaseContext>(options =>
            //            options.UseSqlite(ConnectionStringHelper.BuildSqlite(appsetting.Database),
            //            x => x.MigrationsHistoryTable("__EFMigrationsHistory", "sqlite")));
            //        break;

            //    case DatabaseProvider.MongoDB:
            //        throw new InvalidOperationException("Later");

            //    default:
            //        throw new InvalidOperationException("Unsupported database provider");
            //}
        }
    }
}
