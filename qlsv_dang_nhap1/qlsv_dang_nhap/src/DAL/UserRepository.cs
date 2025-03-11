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

    public DataTable GetAllUsers()
    {
        var DataTable = new DataTable();
        using (var conn = new MySqlConnection(_connectionString))
        {
            conn.Open();
            const string query = "Select * from user";
            using (var adapter = new MySqlDataAdapter(query,conn)){
                adapter.Fill(DataTable);
            }
        }
        return DataTable;
    }

    public void AddUser(User user)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        const string query = "INSERT INTO user (user_name, password, role) VALUES (@username, @password, @role)";
        using var cmd = new MySqlCommand(query, connection);

        cmd.Parameters.AddWithValue("@username", user.Username);
        cmd.Parameters.AddWithValue("@password", user.Password);
        cmd.Parameters.AddWithValue("@role", user.Role);

        cmd.ExecuteNonQuery();
    }

    public void DeleteUser(int userId)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        const string query = "DELETE FROM user WHERE id = @id";
        using var cmd = new MySqlCommand(query, connection);

        cmd.Parameters.AddWithValue("@id", userId);
        cmd.ExecuteNonQuery();
    }

    public void UpdatePassword(string username, string newPassword)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        const string query = "UPDATE user SET password = @password WHERE user_name = @username";
        using var cmd = new MySqlCommand(query, connection);

        cmd.Parameters.AddWithValue("@password", newPassword);
        cmd.Parameters.AddWithValue("@username", username);

        cmd.ExecuteNonQuery();
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
            Id = reader.GetInt32("id"),
            Username = reader.GetString("user_name"),
            Password = reader.GetString("password"),
            Role = reader.GetInt32("role")
        } : null;
    }
}