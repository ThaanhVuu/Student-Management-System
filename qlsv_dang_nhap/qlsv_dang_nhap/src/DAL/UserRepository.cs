using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

public class UserRepository
{
    private readonly string _connectionString;

    public UserRepository(string connectionString)
    {
        _connectionString = ConfigurationManager.ConnectionStrings["SMS"].ConnectionString;
    }

    public User? GetUserByUsername(string username)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        const string query = "SELECT * FROM user WHERE user_name = @username";
        using var cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@username", username);

        using var reader = cmd.ExecuteReader();
        return reader.Read() ? new User
        {
            Id = reader.GetInt64("id"),
            Username = reader.GetString("user_name"),
            Password = reader.GetString("password"),
            Role = reader.GetInt32("role")
        } : null;
    }
}