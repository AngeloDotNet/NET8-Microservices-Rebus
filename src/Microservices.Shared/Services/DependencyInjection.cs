namespace Microservices.Shared.Services;

public static class DependencyInjection
{
    private static readonly string dbhostname = "YOUR-HOST";
    private static readonly string dbdatabase = "YOUR-DATABASE";
    private static readonly string dbusername = "YOUR-USER";
    private static readonly string dbpassword = "YOUR-PASSWORD";

    public static readonly string database = $"Data Source={dbhostname};Initial Catalog={dbdatabase};User ID={dbusername};Password={dbpassword};Encrypt=False";
    public static readonly string rabbitConnectionString = "amqp://guest:guest@localhost";
}