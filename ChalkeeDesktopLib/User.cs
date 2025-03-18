using System.Data;

namespace ChalkeeDesktopLib;

public class User(
    Guid id,
    string email,
    string password,
    string firstName,
    string lastName,
    Guid institutionId,
    Guid? classId,
    string role,
    string? studentId)
{
    public readonly Guid ID = id;
    public string Email { init; get; } = email;
    public string Password { init; get; } = password;
    public string FirstName { init; get; } = firstName;
    public string LastName { init; get; } = lastName;
    public Guid InstitutionId { init; get; } = institutionId;
    public Guid? ClassId { init; get; } = classId;
    public string Role { init; get; } = role;
    public string? StudentId { init; get; } = studentId;
}

//public static class User
//{

//    public static Guid ID { get; private set; }
//    public static string Email { get; private set; }
//    public static string Password { get; private set; }
//    public static string FirstName { get; private set; }
//    public static string LastName { get; private set; }
//    public static Guid InstitutionId { get; private set; }
//    public static Guid? ClassId { get; private set; }
//    public static string Role { get; private set; }
//    public static string? StudentId { get; private set; }




//    public static void SetPropeties(Guid id,
//                string email,
//                string password,
//                string firstName,
//                string lastName,
//                Guid institutionId,
//                Guid? classId,
//                string role,
//                string? studentId)
//    {


//        ID = id;
//        Email = email;
//        Password = password;
//        FirstName = firstName;
//        LastName = lastName;
//        InstitutionId = institutionId;
//        ClassId = classId;
//        Role = role;
//        StudentId = studentId;

//    }




//}





