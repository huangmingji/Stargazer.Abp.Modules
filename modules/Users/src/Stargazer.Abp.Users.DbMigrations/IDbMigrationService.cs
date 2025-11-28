namespace Stargazer.Abp.Users.DbMigrations;

public interface IDbMigrationService
{
    Task MigrateAsync();
}