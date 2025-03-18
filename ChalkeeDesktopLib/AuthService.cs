using Npgsql;
using Isopoh.Cryptography.Argon2;
namespace ChalkeeDesktopLib;
public class AuthService(NpgsqlDataSource dataSource)
{
    public async Task<User?> TrySignIn(string emailOrSid, string pass)
    {
        await using var cmd = dataSource.CreateCommand("SELECT * FROM users WHERE email = @email_or_sid OR student_id = @email_or_sid");
        cmd.Parameters.AddWithValue("email_or_sid", emailOrSid);

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            if (reader.HasRows && Argon2.Verify((string)reader["password"], pass))
            {
                return new User(
                    (Guid)reader["id"],
                    (string)reader["email"],
                    (string)reader["password"],
                    (string)reader["first_name"],
                    (string)reader["last_name"],
                    (Guid)reader["institution_id"],
                    reader["class_id"].GetType() == typeof(System.DBNull)! ? null : (Guid)reader["class_id"],
                    (string)reader["role"], 
                    reader["student_id"].GetType() == typeof(System.DBNull)! ? null : (string)reader["student_id"]
                );
            }
        }
        
        return null;
    }
}