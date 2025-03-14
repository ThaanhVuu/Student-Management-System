using System;
using System.Data;
using MySql.Data.MySqlClient;
using qlsv_dang_nhap;

    public class StudentRepository
    {
        // Thay YOUR_SERVER, YOUR_DATABASE, YOUR_USER, YOUR_PASSWORD bằng...
        private static string connectionString = "Server=YOUR_SERVER;Database=YOUR_DATABASE;User=YOUR_USER;Password=YOUR_PASSWORD;";

        public static Student GetStudentById(string MaSV)
        {
            Student student = null;
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
                            student = new Student
                            {
                                MaSV = reader["MaSV"].ToString(),
                                HoTen = reader["HoTen"].ToString(),
                                NgaySinh = reader["NgaySinh"].ToString(),
                                GioiTinh = reader["GioiTinh"].ToString(),
                                Lop = reader["Lop"].ToString(),
                                Nganh = reader["Nganh"].ToString(),
                                Email = reader["Email"].ToString(),
                                SDT = reader["SDT"].ToString(),
                                TrangThai = reader["TrangThai"].ToString()
                            };
                        }
                    }
                }
            }
            return student;
        }
    }
