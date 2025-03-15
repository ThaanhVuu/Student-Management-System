// StudentRepository.cs
using MySql.Data.MySqlClient;
using System.Windows;
using qlsv_dang_nhap.srcMVC.model;
using System.Configuration;

namespace qlsv_dang_nhap.srcMVC.model
{
    public class StudentRepository
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["sms"].ConnectionString;

        internal static StudentMVC GetStudentById(string MaSV)
        {
            StudentMVC student = null;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT a.admission_id, s.full_name, s.date_of_birth, s.gender, s.class_name, p.program_name,s.student_status FROM student s inner join program p on p.program_id = s.program_id inner join admission a on a.admission_id = s.admission_id WHERE a.admission_id = @MaSV";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSV", MaSV);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                student = new StudentMVC
                                {
                                    MaSV = reader["admission_id"]?.ToString(),
                                    HoTen = reader["full_name"]?.ToString(),
                                    NgaySinh = reader["date_of_birth"]?.ToString(),
                                    GioiTinh = reader["gender"]?.ToString(),
                                    Lop = reader["class_name"]?.ToString(),
                                    Nganh = reader["program_name"]?.ToString(),
                                    TrangThai = reader["student_status"]?.ToString()
                                };
                            }
                            else
                            {
                                throw new Exception($"Không tìm thấy sinh viên với MaSV = {MaSV}");
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