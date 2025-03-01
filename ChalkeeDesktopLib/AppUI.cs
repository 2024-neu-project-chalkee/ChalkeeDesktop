using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
namespace ChalkeeDesktopLib;
public class AppUI(AuthService authService)
{
    private User? User { get; set; } = null;
    public async Task Run()
    {
        if (!await SignInMenu()) return;
        ShowDashboard();
    }

    private async Task<bool> SignInMenu()
    {
        bool signInSuccess = true;
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

            User = await authService.TrySignIn(emailOrSid, pass);
            
            if (User != null)
            {
                return true;
            }
            else
            {
                signInSuccess = false;
            }
        }
    }

    private void ShowDashboard()
    {
        string[] options = [];
        switch (User!.Role)
        {
            case "Student":
            options = ["My information", "My timetables", "My grades", "Sign out"];
            break;
            case "Teacher":
                options = ["My information", "My timetables", "Sign out"];
            break;
            case "Principal":
                options = ["My information", "My timetables", "Manage timetables", "Sign out"];
            break;
            case "Admin":
                options = ["Sign out"];
            break;
        }
        int selectedIndex = 0;
        Console.CursorVisible = false;
        
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Welcome back, {User!.FirstName}!\nUse the up and down arrows to navigate, ENTER to select.\n");

            for (int i = 0; i < options.Length; i++)
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
                    HandleMenuSelection(options[selectedIndex]);
                    return;
            }
        }
    }

    private void HandleMenuSelection(string selection)
    {
        Console.Clear();
        switch (selection)
        {
            case "My information":
                Console.WriteLine("Your information should be displayed here soon!");
                break;
            case "My grades":
                Console.WriteLine("Your grades should be displayed here soon!");
                break;
            case "My timetables":
                Console.WriteLine("Your timetables should be displayed here soon!");
                break;
            case "Manage timetables":
                Console.WriteLine("Timetable management should be displayed here soon!");
                break;
            case "Sign out":
                Console.WriteLine("Signing out...");
                Task.Delay(1000).Wait();
                Run().Wait();
                return;
        }
        Console.WriteLine("\nPress any key to return...");
        Console.ReadKey();
        ShowDashboard();
    }

    private static string ReadPassword()
    {
        string pass = string.Empty;
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
}