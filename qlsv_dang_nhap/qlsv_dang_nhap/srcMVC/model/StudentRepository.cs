// StudentRepository.cs
using MySql.Data.MySqlClient;
using System.Windows;
using qlsv_dang_nhap.srcMVC.model;

namespace qlsv_dang_nhap.srcMVC.model
{
    public class StudentRepository
    {
        private static string connectionString = "Server=YOUR_SERVER;Database=YOUR_DATABASE;User=YOUR_USER;Password=YOUR_PASSWORD;";

        internal static StudentMVC GetStudentById(string MaSV)
        {
            StudentMVC student = null;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MaSV, HoTen, NgaySinh, GioiTinh, Lop, Nganh, Email, SDT, TrangThai FROM SinhVien WHERE MaSV = @MaSV";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSV", MaSV);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                student = new StudentMVC
                                {
                                    MaSV = reader["MaSV"]?.ToString(),
                                    HoTen = reader["HoTen"]?.ToString(),
                                    NgaySinh = reader["NgaySinh"]?.ToString(),
                                    GioiTinh = reader["GioiTinh"]?.ToString(),
                                    Lop = reader["Lop"]?.ToString(),
                                    Nganh = reader["Nganh"]?.ToString(),
                                    Email = reader["Email"]?.ToString(),
                                    SDT = reader["SDT"]?.ToString(),
                                    TrangThai = reader["TrangThai"]?.ToString()
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