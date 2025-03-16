using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.IsisMtt.X509;
using System.Configuration;
using System.Data;

class StudentRepository
{
    string _connectionString;
    public StudentRepository()
    {
        _connectionString = ConfigurationManager.ConnectionStrings["SMS"].ConnectionString;
    }
    public void AddStudent(Student student)
    {
        using (var conn = new MySqlConnection(_connectionString))
        {
            string query = "INSERT INTO student (admission_id, full_name, date_of_birth, gender, program_id, class_name, student_status) VALUES (@admission_id, @full_name, @date_of_birth, @gender, @program_id, @class_name, @status)";
            conn.Open();
            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@admission_id", student.Id);
                cmd.Parameters.AddWithValue("@full_name", student.Name);
                cmd.Parameters.AddWithValue("@date_of_birth", student.Dob);
                cmd.Parameters.AddWithValue("@gender", student.gender);
                cmd.Parameters.AddWithValue("@program_id", student.ProgramId);
                cmd.Parameters.AddWithValue("@class_name", student.ClassName);
                cmd.Parameters.AddWithValue("@status", student.Status);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void UpdateStudent(Student student)
    {
        using(var conn = new MySqlConnection(_connectionString))
        {
            const string query = "UPDATE student SET student_status = @student_status,class_name = @classname, program_id = @program_id, full_name = @full_name, date_of_birth = @date_of_birth, gender = @gender WHERE admission_id = @admission_id";
            conn.Open();
            using var command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@classname", student.ClassName);
            command.Parameters.AddWithValue("@program_id", student.ProgramId);
            command.Parameters.AddWithValue("@full_name", student.Name);
            command.Parameters.AddWithValue("@date_of_birth", student.Dob);
            command.Parameters.AddWithValue("@gender", student.gender);
            command.Parameters.AddWithValue("@student_status", student.Status);
            command.Parameters.AddWithValue("@admission_id", student.Id);
            command.ExecuteNonQuery();
        }
    }

    public void DeleteStudent(Student student)
    {
        using (var conn = new MySqlConnection(_connectionString))
        {
            string query = "delete from student where admission_id = @id;";
            conn.Open();
            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id",student.Id);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public DataTable GetStudents()
    {
        var DataTable = new DataTable();
        using (var conn = new MySqlConnection(_connectionString))
        {
            conn.Open();
            const string query = "Select s.admission_id, s.full_name, s.date_of_birth, s.gender, p.program_name, p.program_id, s.class_name, s.student_status from student s inner join program p on s.program_id = p.program_id order by admission_id desc";
            using (var adapter = new MySqlDataAdapter(query, conn))
            {
                adapter.Fill(DataTable);
            }
        }
        return DataTable;
    }

    public DataTable? SearchStudent(string keyword)
    {
        var dataTable = new DataTable();
        using (var conn = new MySqlConnection(_connectionString))
        {
            conn.Open();
            string query = @"
            Select 
            s.admission_id,
            s.full_name,
            s.date_of_birth,
            s.gender,
            p.program_name,
            p.program_id,
            s.class_name,
            s.student_status
            from student s
            inner join program p on p.program_id = s.program_id
            inner join admission a on a.admission_id = s.admission_id
            where s.full_name like @keyword
            or s.admission_id like @keyword
            or s.date_of_birth like @keyword
            or p.program_name like @keyword
            or s.student_status like @keyword
            or s.class_name like @keyword
            or s.gender like @keyword
            ";
            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                using (var adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(dataTable);
                }
            }
        }
        return dataTable;
    }
}

