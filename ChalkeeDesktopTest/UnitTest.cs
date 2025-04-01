using System.Formats.Asn1;
using Npgsql;
using Microsoft.Extensions.Configuration;
using ChalkeeDesktopLib;
namespace ChalkeeDesktopTest;

[TestClass]
public sealed class UnitTest
{
    
    
    
    
    [TestMethod]
    public void UserCreationTest()
    {
        User user = new(Guid.NewGuid(), "test@example.com", "password", "Test", "Name", Guid.NewGuid(), Guid.NewGuid(), "Student", "7xxxxxxxxxx");
        Assert.AreEqual("Test", user.FirstName);
    }


    [TestMethod]
    public async Task MyInformtion_CheckExcpetionMessage()
    {
        
        
        /*
         * 
        var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appettings.json").Build();

        var connectionString = configuration.GetConnectionString("ChalkeeDB");
        await using var dataSource = NpgsqlDataSource.Create(connectionString!);

        
        var authService = new AuthService(dataSource);
        var appUI = new AppUI(authService);
         */

        await AppUI.LoadMyInformation();
        
        Assert.AreEqual("The configuration file 'appsettings.json' was not found and is not optional. The expected physical path was 'C:\\Users\\Móric2\\OneDrive\\Dokumentumok\\Neu12b\\ikt\\Aminisztrációs projekt (NYÁRON CSINÁLNI)\\ChalkeeConsole\\ChalkeeDesktop\\ChalkeeDesktopTest\\bin\\Debug\\net9.0\\appsettings.json'.", AppUI._results_for_testing[0]);
        
        




    }

    
}
