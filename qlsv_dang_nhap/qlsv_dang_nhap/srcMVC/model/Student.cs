using System;

namespace qlsv_dang_nhap.srcMVC.model
{
    public class Student
    {
        public string? MaSV { get; set; }
        public string? HoTen { get; set; }
        public string? NgaySinh { get; set; }
        public string? GioiTinh { get; set; }
        public string? Lop { get; set; }
        public string? Nganh { get; set; }
        public string? Email { get; set; }
        public string? SDT { get; set; }
        public string? TrangThai { get; set; }
        public static string? LoggedInMaSV { get; set; }
    }
}
