using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
public class ProgramRepository
{
    private string _connectionString;

    public ProgramRepository()
    {
        _connectionString = ConfigurationManager.ConnectionStrings["SMS"].ConnectionString;
    }
    public DataTable GetAllPrograms()
    {
        var DataTable = new DataTable();
        using (var conn = new MySqlConnection(_connectionString))
        {
            conn.Open();
            const string query = "Select * from program";
            using (var adapter = new MySqlDataAdapter(query, conn))
            {
                adapter.Fill(DataTable);
            }
        }
        return DataTable;
    }

    public void AddProgram(Program program)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        const string query = "INSERT INTO program (program_id, program_name) VALUES (@program_id, @program_name)";
        using var cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@program_id", program.ProgramID);
        cmd.Parameters.AddWithValue("@program_name", program.ProgramName);
        cmd.ExecuteNonQuery();
    }

    public void DeleteProgram(Program program)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        const string query = "DELETE FROM program WHERE program_id = @id";
        using var cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@id", program.ProgramID);
        cmd.ExecuteNonQuery();
    }

    public void UpdateProgramName(Program program)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        const string query = "UPDATE program SET program_name = @program_name WHERE program_id = @id";
        using var cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@program_name", program.ProgramName);
        cmd.Parameters.AddWithValue("@id", program.ProgramID);
        cmd.ExecuteNonQuery();
    }

    public Program? GetProgramById(string programName, long programId)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        const string query = "SELECT * FROM program WHERE program_name = @programName";
        using var cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@programName", programName);
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Program
            {
                ProgramID = reader.GetInt32("id"),
                ProgramName = reader.GetString("program_name")
            };
        }
        return null;
    }
}
