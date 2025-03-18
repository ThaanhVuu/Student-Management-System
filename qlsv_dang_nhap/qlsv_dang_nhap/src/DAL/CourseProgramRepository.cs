using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

class CourseProgramRepository
{
    private readonly string _connection;

    public CourseProgramRepository()
    {
        _connection = ConfigurationManager.ConnectionStrings["sms"].ConnectionString;
    }

    public DataTable getall(CourseProgram cp)
    {
        DataTable dt = new DataTable();
        using (var conn = new MySqlConnection(_connection))
        {
            conn.Open();
            string query = $"select c.course_id, p.program_id, p.program_name, c.course_name from course_program cs inner join program p on p.program_id = cs.program_id inner join course c on c.course_id = cs.course_id where p.program_id = {cp.program_id}";
            using var adapter = new MySqlDataAdapter(query, conn);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            dt.PrimaryKey = new DataColumn[] { dt.Columns["program_id"] };
            adapter.Fill(dt);
        }
        return dt;
    }

    public void deleteCP(int id)
    {
        using(var conn = new MySqlConnection(_connection))
        {
            conn.Open();
            string query = $"delete from course_program where course_id = {id}";
            using var cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
    
    public void addCP(int id, long idp)
    {
       using (var conn = new MySqlConnection(_connection))
        {
            conn.Open();
            string query = $"insert into course_program values({idp},{id})";
            using var cmd = new MySqlCommand( query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }

}
