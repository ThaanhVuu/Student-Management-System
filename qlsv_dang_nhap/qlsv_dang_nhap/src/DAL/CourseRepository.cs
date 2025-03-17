using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

public class CourseRepository
{
    string _connectionString;

    public CourseRepository()
    {
        _connectionString = ConfigurationManager.ConnectionStrings["sms"].ConnectionString;
    }

    public DataTable getCourse()
    {
        DataTable dt = new DataTable();
        using(var conn = new MySqlConnection(_connectionString))
        {
            string query = "select course_id, course_name, credit from course";
            conn.Open();
            using var adapter = new MySqlDataAdapter(query, conn);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            adapter.Fill(dt);
        }
        return dt;
    }

    public void UpdateCourse(DataTable dt)
    {
        using (var conn = new MySqlConnection(_connectionString))
        {
            conn.Open();
            string query = "select course_id, course_name, credit from course";
            var adapter = new MySqlDataAdapter(query, conn);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            var builder = new MySqlCommandBuilder(adapter);
            adapter.Update(dt);   
        }
    }
}