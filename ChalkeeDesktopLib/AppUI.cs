using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

using Npgsql;

namespace ChalkeeDesktopLib;
public class AppUI(AuthService authService)
{
    public static User? CurrentUser { get; set; } = null;
    public async Task Run()
    {
        if (!await SignInMenu()) return;
        await ShowDashboard();
    }

    private async Task<bool> SignInMenu()
    {
        var signInSuccess = true;
        Console.CursorVisible = true;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588                                         \u2588\u2588\u2588         \u2588\u2588       \u2588\u2588   \u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588 \u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\n\u2588\u2588                       \u2588\u2588\u2588\u2588          \u2588\u2588\u2588\u2588\u2588           \u2588\u2588\u2588         \u2588\u2588     \u2588\u2588\u2588    \u2588\u2588             \u2588\u2588\u2588           \n\u2588\u2588                     \u2588\u2588\u2588\u2588          \u2588\u2588\u2588  \u2588\u2588\u2588\u2588         \u2588\u2588\u2588         \u2588\u2588   \u2588\u2588\u2588      \u2588\u2588             \u2588\u2588\u2588           \n\u2588\u2588                   \u2588\u2588\u2588\u2588          \u2588\u2588\u2588      \u2588\u2588\u2588\u2588       \u2588\u2588\u2588         \u2588\u2588 \u2588\u2588\u2588        \u2588\u2588             \u2588\u2588\u2588           \n\u2588\u2588                 \u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588     \u2588\u2588\u2588         \u2588\u2588\u2588\u2588          \u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588 \u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\n\u2588\u2588               \u2588\u2588\u2588\u2588          \u2588\u2588\u2588              \u2588\u2588\u2588\u2588   \u2588\u2588\u2588         \u2588\u2588\u2588\u2588\u2588\u2588        \u2588\u2588             \u2588\u2588\u2588           \n\u2588\u2588             \u2588\u2588\u2588           \u2588\u2588\u2588                  \u2588\u2588\u2588\u2588 \u2588\u2588\u2588         \u2588\u2588  \u2588\u2588\u2588\u2588      \u2588\u2588             \u2588\u2588\u2588           \n\u2588\u2588           \u2588\u2588\u2588           \u2588\u2588\u2588                      \u2588\u2588\u2588\u2588\u2588\u2588         \u2588\u2588    \u2588\u2588\u2588\u2588    \u2588\u2588             \u2588\u2588\u2588           \n\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588           \u2588\u2588\u2588                           \u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588      \u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\n\n");
            Console.WriteLine(signInSuccess ? "Please sign in to continue..." : "Incorrect credentials!");
            Console.Write("Email or Student ID: ");
            var emailOrSid = Console.ReadLine()!;

            Console.Write("Password: ");
            var pass = ReadPassword();

            CurrentUser = await authService.TrySignIn(emailOrSid, pass);
            
            if (CurrentUser != null && CurrentUser!.Role == "Student")
            {
                return true;
            }
            else
            {
                signInSuccess = false;
            }
        }
    }

    private async Task ShowDashboard()
    {
        string[] options = ["Test", "My information", "My timetables", "My grades", "Sign out"];
        var selectedIndex = 0;
        Console.CursorVisible = false;
        
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Welcome back, {CurrentUser!.FirstName}!\nUse the up and down arrows to navigate, ENTER to select.\n");

            for (var i = 0; i < options.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }

                Console.WriteLine($" {options[i]} ");

                Console.ResetColor();
            }

            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex = (selectedIndex == 0) ? options.Length - 1 : selectedIndex - 1;
                    break;
                case ConsoleKey.DownArrow:
                    selectedIndex = (selectedIndex == options.Length - 1) ? 0 : selectedIndex + 1;
                    break;
                case ConsoleKey.Enter:
                    await HandleMenuSelection(options[selectedIndex]);
                    return;
                default:
                    return;
            }
        }
    }

    private async Task HandleMenuSelection(string selection)
    {
        Console.Clear();
        switch (selection)
        {
            case "Test":
                await LoadTimetables();
                break;
            case "My information":
                //Console.WriteLine("Your information should be displayed here soon!");
                await LoadMyInformation();
                break;
            case "My grades":
                Console.WriteLine("Your grades should be displayed here soon!");
                break;
            case "My timetables":
                Console.WriteLine("Your timetables should be displayed here soon!");
                break;
            case "Sign out":
                Console.WriteLine("Signing out...");
                Task.Delay(1000).Wait();
                Run().Wait();
                return;
        }
        Console.WriteLine("\nPress any key to return...");
        Console.ReadKey();
        await ShowDashboard();
    }

    private static string ReadPassword()
    {
        var pass = string.Empty;
        ConsoleKey key;
        do
        {
            var keyInfo = Console.ReadKey(intercept: true);
            key = keyInfo.Key;

            if (key == ConsoleKey.Backspace && pass.Length > 0)
            {
                Console.Write("\b \b");
                pass = pass[0..^1];
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                Console.Write("*");
                pass += keyInfo.KeyChar;
            }
        } while (key != ConsoleKey.Enter);

        Console.WriteLine();
        return pass;
    }


    private static async Task LoadTimetables()
    {
        try
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();

            var connectionString = configuration.GetConnectionString("ChalkeeDB");
            await using var dataSource = NpgsqlDataSource.Create(connectionString!);


            await using var command = dataSource.CreateCommand("SELECT name FROM subjects");
            await using var reader = await command.ExecuteReaderAsync();






            while (await reader.ReadAsync())
            {
                Console.WriteLine(reader.GetString(0));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Oops, an unexpected error occured!");
        }
    }

    private static async Task LoadMyInformation()
    {
        try
        {

            

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();

            var connectionString = configuration.GetConnectionString("ChalkeeDB");
            await using var dataSource = NpgsqlDataSource.Create(connectionString!);

            await using var command = dataSource.CreateCommand("SELECT institutions.name, institutions.location, institutions.website, institutions.phone_number FROM users JOIN institutions ON users.institution_id = institutions.id WHERE users.id = @user_id");
            command.Parameters.AddWithValue("user_id", CurrentUser!.ID);
            await using var reader = await command.ExecuteReaderAsync();






            while (await reader.ReadAsync())
            {
                Console.WriteLine(reader.GetString(0));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Oops, an unexpected error occured!");
            Console.WriteLine(e.Message);
            
        }
    }
    
    
}