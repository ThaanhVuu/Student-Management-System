// StudentRepository.cs
using MySql.Data.MySqlClient;
using System.Windows;
using System.Configuration;

namespace qlsv_dang_nhap.srcMVC
{
    public class StudentRepositoryMVC
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["sms"].ConnectionString;

        internal static StudentMVC GetStudentById(string loggedInMaSV)
        {
            StudentMVC student = null;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT student.admission_id, full_name, date_of_birth, gender, class_name, program_name, student_status FROM student inner join program on program.program_id = student.program_id WHERE student.admission_id = @MaSV";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSV", loggedInMaSV);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                student = new StudentMVC
                                {
                                    MaSV = reader["admission_id"]?.ToString(),
                                    HoTen = reader["full_name"]?.ToString(),
                                    NgaySinh = DateTime.TryParse(reader["date_of_birth"]?.ToString(), out DateTime ngaySinh)
                                        ? ngaySinh.ToString("yyyy/MM/dd")
                                        : null,
                                    GioiTinh = reader["gender"]?.ToString(),
                                    Lop = reader["class_name"]?.ToString(),
                                    Nganh = reader["program_name"]?.ToString(),
                                    TrangThai = reader["student_status"]?.ToString()
                                };
                            }
                            else
                            {
                                throw new Exception($"Không tìm thấy sinh viên với MaSV = {loggedInMaSV}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi truy vấn CSDL: {ex.Message}");
                return null;
            }
            return student;
        }
    }
}