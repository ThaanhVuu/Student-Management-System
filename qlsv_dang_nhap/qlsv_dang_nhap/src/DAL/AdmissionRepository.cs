using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
public class AdmissionRepository
{
    private readonly string _connectionString;
    public AdmissionRepository(string connectionString)
    {
        _connectionString = ConfigurationManager.ConnectionStrings["SMS"].ConnectionString;
    }
    //lay ds
    public DataTable GetAllAdmissions()
    {
        var DataTable = new DataTable();
        using (var conn = new MySqlConnection(_connectionString))
        {
            conn.Open();
            const string query = "Select a.admission_id, p.program_id, p.program_name, a.full_name, a.date_of_birth, a.gender, a.admission_status from admission a inner join program p on a.program_id = p.program_id";
            using (var adapter = new MySqlDataAdapter(query, conn))
            {
                adapter.Fill(DataTable);
            }
        }
        return DataTable;
    }
    //them
    public async Task<int> AddAdmissionAsync(Admission admission)
    {
        using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync(); // Mở kết nối bất đồng bộ

        // Sử dụng câu truy vấn kết hợp INSERT và SELECT LAST_INSERT_ID()
        const string query = @"
        INSERT INTO admission (program_id, full_name, date_of_birth, gender, admission_status) 
        VALUES (@program_id, @full_name, @date_of_birth, @gender, @admission_status);
        SELECT LAST_INSERT_ID();";

        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@program_id", admission.ProgramId);
        command.Parameters.AddWithValue("@full_name", admission.FullName);
        command.Parameters.AddWithValue("@date_of_birth", admission.DOB);
        command.Parameters.AddWithValue("@gender", admission.Gender);
        command.Parameters.AddWithValue("@admission_status", admission.StatusAdmission);

        // Thực thi và lấy giá trị admission_id tự tăng
        var newAdmissionId = Convert.ToInt32(await command.ExecuteScalarAsync());
        return newAdmissionId;
    }

    //sua
    public void UpdateAdmission(Admission admission)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        const string query = "UPDATE admission SET program_id = @program_id, full_name = @full_name, date_of_birth = @date_of_birth, gender = @gender WHERE admission_id = @admission_id";
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@program_id", admission.ProgramId);
        command.Parameters.AddWithValue("@full_name", admission.FullName);
        command.Parameters.AddWithValue("@date_of_birth", admission.DOB);
        command.Parameters.AddWithValue("@gender", admission.Gender);
        command.Parameters.AddWithValue("@admission_status", admission.StatusAdmission);
        command.Parameters.AddWithValue("@admission_id", admission.AdmissionId);
        int row = command.ExecuteNonQuery();
        if (row == 0)
        {
            throw new Exception("Không tìm thấy hồ sơ cần sửa");
        }
    }

    //xoa
    public void DeleteAdmission(long admissionId)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        const string query = "DELETE FROM admission WHERE admission_id = @admission_id";
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@admission_id", admissionId);
        command.ExecuteNonQuery();
    }

    //duyet
    public void ApproveAdmission(long admissionId)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        const string query = "UPDATE admission SET admission_status = 'Approved' WHERE admission_id = @admission_id";
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@admission_id", admissionId);
        command.ExecuteNonQuery();
    }

    //tu choi
    public void RejectAdmission(long admissionId)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        const string query = "UPDATE admission SET admission_status = 'Rejected' WHERE admission_id = @admission_id";
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@admission_id", admissionId);
        command.ExecuteNonQuery();
    }

    //tim kiem
    public DataTable? SearchAdmission(string keyword)
    {
        var dataTable = new DataTable();
        using (var conn = new MySqlConnection(_connectionString))
        {
            conn.Open();
            string query = @"
            SELECT 
                a.admission_id, 
                p.program_id,
                p.program_name, 
                a.full_name, 
                a.date_of_birth, 
                a.gender, 
                a.admission_status 
            FROM admission a 
            INNER JOIN program p ON a.program_id = p.program_id 
            WHERE p.program_name LIKE @keyword 
                OR a.full_name LIKE @keyword 
                OR DATE_FORMAT(a.date_of_birth, '%Y-%m-%d') LIKE @keyword 
                OR a.gender LIKE @keyword 
                OR a.admission_status LIKE @keyword";

            using (var cmd = new MySqlCommand(query, conn))
            {
                Console.WriteLine($"Searching with keyword: {keyword}"); // Log keyword
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