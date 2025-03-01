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
    public Guid ID { init; get; } = id;
    public string Email { init; get; } = email;
    public string Password { init; get; } = password;
    public string FirstName { init; get; } = firstName;
    public string LastName { init; get; } = lastName;
    public Guid InstitutionId { init; get; } = institutionId;
    public Guid? ClassId { init; get; } = classId;
    public string Role { init; get; } = role;
    public string? StudentId { init; get; } = studentId;
}