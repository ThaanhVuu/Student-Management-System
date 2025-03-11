using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace qlsv_dang_nhap.DAL
{
    public class SinhVienDAL
    {
    private readonly string _connectionString;

    public SinhVienDAL(string connectionString)
    {
        _connectionString = ConfigurationManager.ConnectionStrings["SMS"].ConnectionString;
    }

        public SinhVienDTO GetSinhVienByMaSV(string maSV)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM SinhVien WHERE MaSinhVien = @MaSinhVien";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSinhVien", maSV);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new SinhVienDTO
                            {
                                MaSinhVien = reader["MaSinhVien"].ToString(),
                                HoVaTen = reader["HoVaTen"].ToString(),
                                NgaySinh = Convert.ToDateTime(reader["NgaySinh"]),
                                GioiTinh = reader["GioiTinh"].ToString(),
                                ChuyenNganh = reader["ChuyenNganh"].ToString(),
                                SoDienThoai = reader["SoDienThoai"].ToString(),
                                Email = reader["Email"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

        public bool UpdateSinhVienContact(string maSV, string sdt, string email)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE SinhVien SET SoDienThoai = @SoDienThoai, Email = @Email WHERE MaSinhVien = @MaSinhVien";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SoDienThoai", sdt);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@MaSinhVien", maSV);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}