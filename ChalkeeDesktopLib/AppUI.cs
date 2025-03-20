using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

using Npgsql;

namespace ChalkeeDesktopLib;
public class AppUI(AuthService authService)
{
    private static User? CurrentUser { get; set; } = null;
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
        string[] options = ["My information", "My timetables", "My grades", "Sign out"];
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
           // case "Test":
             //   
               // break;
            case "My information":
                await LoadMyInformation();
                break;
            case "My grades":
                Console.WriteLine("Your grades should be displayed here soon!");
                break;
            case "My timetables":
                //Console.WriteLine("Your timetables should be displayed here soon!");
                await LoadTimetables();
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


    private static async Task LoadMyTimetables()
    {
        try
        {

            List<Timetable> dailySchedules= [];
            
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();

            var connectionString = configuration.GetConnectionString("ChalkeeDB");
            await using var dataSource = NpgsqlDataSource.Create(connectionString!);


	        var queryString = """
	                          
	                          			WITH user_role AS (
	                          				SELECT role 
	                          				FROM users 
	                          				WHERE id = @user_id
	                          			), 
	                          			timetable_combined AS (
	                          				SELECT 
	                          					tc.day, 
	                          					tc.period, 
	                          					s2.name AS subject,
	                          					CONCAT(u2.first_name, ' ', u2.last_name) AS name,
	                          					r2.name AS room,
	                          					CASE 
	                          						WHEN c2.id IS NULL THEN NULL 
	                          						ELSE CONCAT(c2.number, '.', c2.letter) 
	                          					END AS class,
	                          					g2.name AS "group",
	                          					gr2.name AS grouproom,
	                          					cr2.name AS classroom,
	                          					tc.status,
	                          					c2.id AS class_id,
	                          					g2.id AS group_id,
	                          					u2.id AS teacher_id
	                          				FROM timetable_changes tc
	                          				LEFT JOIN classes c2 ON tc.class_id = c2.id
	                          				LEFT JOIN groups g2 ON tc.group_id = g2.id
	                          				LEFT JOIN users u2 ON tc.teacher_id = u2.id
	                          				LEFT JOIN rooms r2 ON tc.room_id = r2.id
	                          				LEFT JOIN subjects s2 ON tc.subject_id = s2.id
	                          				LEFT JOIN rooms cr2 ON c2.classroom = cr2.id AND c2.id IS NOT NULL
	                          				LEFT JOIN rooms gr2 ON g2.grouproom = gr2.id AND g2.id IS NOT NULL
	                          
	                          				UNION ALL
	                          
	                          				SELECT 
	                          					t.day, 
	                          					t.period, 
	                          					s1.name AS subject,
	                          					CONCAT(u1.first_name, ' ', u1.last_name) AS name,
	                          					r1.name AS room,
	                          					CASE 
	                          						WHEN c1.id IS NULL THEN NULL 
	                          						ELSE CONCAT(c1.number, '.', c1.letter) 
	                          					END AS class,
	                          					g1.name AS "group",
	                          					gr1.name AS grouproom,
	                          					cr1.name AS classroom,
	                          					NULL AS status,
	                          					c1.id AS class_id,
	                          					g1.id AS group_id,
	                          					u1.id AS teacher_id
	                          				FROM timetables t
	                          				LEFT JOIN classes c1 ON t.class_id = c1.id
	                          				LEFT JOIN groups g1 ON t.group_id = g1.id
	                          				LEFT JOIN users u1 ON t.teacher_id = u1.id
	                          				LEFT JOIN rooms r1 ON t.room_id = r1.id
	                          				LEFT JOIN subjects s1 ON t.subject_id = s1.id
	                          				LEFT JOIN rooms cr1 ON c1.classroom = cr1.id AND c1.id IS NOT NULL
	                          				LEFT JOIN rooms gr1 ON g1.grouproom = gr1.id AND g1.id IS NOT NULL
	                          				WHERE NOT EXISTS (
	                          					SELECT 1 FROM timetable_changes tc 
	                          					WHERE tc.day = t.day AND tc.period = t.period
	                          				)
	                          			)
	                          
	                          			SELECT day, period, subject, name, room, class, "group", classroom, grouproom, status
	                          			FROM timetable_combined
	                          			WHERE 
	                          			(
	                          				(SELECT role FROM user_role) = 'Student' 
	                          				AND (
	                          					group_id IN (
	                          						SELECT group_id 
	                          						FROM user_groups 
	                          						WHERE user_id = @user_id
	                          					)
	                          					OR class_id IN (
	                          						SELECT id 
	                          						FROM classes 
	                          						WHERE id = (
	                          							SELECT class_id 
	                          							FROM users 
	                          							WHERE id = @user_id
	                          						)
	                          					)
	                          				)
	                          			) 
	                          			OR 
	                          			(
	                          				(SELECT role FROM user_role) IN ('Teacher', 'Principal') 
	                          				AND (
	                          					teacher_id = @user_id
	                          				)
	                          			);
	                                      
	                          """;
	        
            await using var command = dataSource.CreateCommand(queryString);
            command.Parameters.AddWithValue("user_id", CurrentUser!.ID);
            await using var timetableReader = await command.ExecuteReaderAsync();

            while (await timetableReader.ReadAsync())
            {
                dailySchedules.Add(
                    new Timetable(
                        Convert.ToInt32(timetableReader["day"]),
                        Convert.ToInt32(timetableReader["period"]),
                        timetableReader["subject"].ToString()!,
                        timetableReader["name"].ToString()!,
                        timetableReader["room"].ToString()!,
                        timetableReader["class"].GetType() == typeof(System.DBNull)! ? null : (string)timetableReader["class"],
                        timetableReader["group"].GetType() == typeof(System.DBNull)! ? null : (string)timetableReader["group"],
                        timetableReader["classroom"].GetType() == typeof(System.DBNull)! ? null : (string)timetableReader["classroom"],
                        timetableReader["grouproom"].GetType() == typeof(System.DBNull)! ? null : (string)timetableReader["grouproom"],
                        timetableReader["status"].GetType() == typeof(System.DBNull)! ? null : (string)timetableReader["status"]
                    )
                );
            }

            dailySchedules = [..dailySchedules.OrderBy(x => x.Day).ThenBy(x => x.Period)];

            string[] days = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"];
            
            var groupedSchedules = dailySchedules.GroupBy(x => x.Day);

            foreach (var group in groupedSchedules)
            {
	            var dayIndex = group.Key - 1;

	            if (dayIndex < 0 || dayIndex >= days.Length) continue;
	            Console.WriteLine($"{days[dayIndex]}:");

	            foreach (var schedule in group)
	            {
		            Console.WriteLine(schedule.ToString());
	            }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Oops, an unexpected error occured!");
            Console.WriteLine(e.Message);
        }
    }

    private static async Task LoadMyGrades()
    {
        try
        {


            var configuration = new ConfigurationBuilder()
                .AddJsonFile("C:\\Users\\Móric2\\OneDrive\\Dokumentumok\\Neu12b\\ikt\\Aminisztrációs projekt (NYÁRON CSINÁLNI)\\ChalkeeConsole\\ChalkeeDesktop\\appsettings.json").Build();

            var connectionString = configuration.GetConnectionString("ChalkeeDB");
            await using var dataSource = NpgsqlDataSource.Create(connectionString!);


            string queryString = "SELECT grades.id, grades.announcement_id, grades.grade, grades.weight, announcements.type, announcements.content, announcements.date, CONCAT(users.first_name, ' ', users.last_name) AS name, subjects.name AS subject_name, groups.name AS group_name, CASE   WHEN classes.number IS NULL AND classes.letter IS NULL THEN NULL  ELSE CONCAT(classes.number, '.', classes.letter)  END AS class_name FROM grades JOIN announcements ON announcements.id = grades.announcement_id JOIN users ON users.id = grades.teacher_id JOIN subjects ON subjects.id = grades.subject_id LEFT JOIN groups ON groups.id = announcements.group_id LEFT JOIN classes ON classes.id = announcements.class_id WHERE grades.student_id = $1 ORDER BY announcements.date `, @user_id";

            await using var command = dataSource.CreateCommand(queryString);
            command.Parameters.AddWithValue("user_id", CurrentUser!.ID);
            await using var TimetableReader = await command.ExecuteReaderAsync();








        }
        catch (Exception e)
        {
            Console.WriteLine("Oops, an unexpected error occured!");
            Console.WriteLine(e.Message);
        }



    }




    private static async Task LoadMyInformation()
    {
        try
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var connectionString = configuration.GetConnectionString("ChalkeeDB");
            await using var dataSource = NpgsqlDataSource.Create(connectionString!);

            await using var command = dataSource.CreateCommand("SELECT institutions.name, institutions.location, institutions.website, institutions.phone_number FROM users JOIN institutions ON users.institution_id = institutions.id WHERE users.id = @user_id");
            command.Parameters.AddWithValue("user_id", CurrentUser!.ID);
            await using var institutionReader = await command.ExecuteReaderAsync();

            await using var command2 = dataSource.CreateCommand("SELECT groups.name, groups.grouproom FROM user_groups JOIN groups ON user_groups.group_id = groups.id WHERE user_id = @user_id");
            command2.Parameters.AddWithValue("user_id", CurrentUser!.ID);
            await using var groupReader = await command2.ExecuteReaderAsync();

            await using var command3 = dataSource.CreateCommand("SELECT classes.number, classes.letter FROM users JOIN classes ON users.class_id = classes.id WHERE users.id = @user_id");
            command3.Parameters.AddWithValue("user_id", CurrentUser!.ID);
            await using var classReader = await command3.ExecuteReaderAsync();
            
            Console.WriteLine($"Name: {CurrentUser.FirstName + " " + CurrentUser.LastName}");
            Console.WriteLine($"Email address: {CurrentUser.Email}");
            Console.WriteLine($"Your student ID: {CurrentUser.StudentId}");
            
            while (await classReader.ReadAsync()) Console.WriteLine($"\nClass: {classReader["number"] + "." + classReader["letter"]}");
            
            List<string> groups = [];
            while (await groupReader.ReadAsync()) groups.Add(groupReader["name"].ToString()!);
            Console.WriteLine($"Groups you're in: {string.Join(", ", groups)}");
            
            while (await institutionReader.ReadAsync())
            {
	            Console.WriteLine($"\nYour institution: {institutionReader["name"]}");
	            Console.WriteLine($"Location: {institutionReader["location"]}");
	            Console.WriteLine($"Tel. Number: {institutionReader["phone_number"]}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Oops, an unexpected error occured!");
            Console.WriteLine(e.Message);
        }
    }
}