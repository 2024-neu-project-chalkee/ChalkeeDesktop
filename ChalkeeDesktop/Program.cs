using Microsoft.Extensions.Configuration;
using Npgsql;
using ChalkeeDesktopLib;

var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("C:\\Users\\Móric2\\OneDrive\\Dokumentumok\\Neu12b\\ikt\\Aminisztrációs projekt (NYÁRON CSINÁLNI)\\ChalkeeConsole\\ChalkeeDesktop\\appsettings.json").Build();

var connectionString = configuration.GetConnectionString("ChalkeeDB");
await using var dataSource = NpgsqlDataSource.Create(connectionString!);

var authService = new AuthService(dataSource);
var appUI = new AppUI(authService);

await appUI.Run();