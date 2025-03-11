using System;
using System.Text.RegularExpressions;

namespace qlsv_dang_nhap.BLL
{
    public class SinhVienBLL
    {
        private static SinhVienBLL instance;

        public static SinhVienBLL Instance
        {
            get
            {
                if (instance == null)
                    instance = new SinhVienBLL();
                return instance;
            }
            private set => instance = value;
        }

        private SinhVienBLL() { }

        // Lấy thông tin sinh viên theo mã sinh viên
        public SinhVienDTO GetSinhVienByMaSV(string maSV)
        {
            if (string.IsNullOrEmpty(maSV))
                return null;

            return SinhVienDAL.Instance.GetSinhVienByMaSV(maSV);
        }

        // Kiểm tra tính hợp lệ của số điện thoại
        public bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return false;

            string pattern = @"^(0|\+84)(\d{9,10})$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

        // Kiểm tra tính hợp lệ của email
        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        // Cập nhật thông tin liên lạc sinh viên (SĐT và Email)
        public Tuple<bool, string> UpdateSinhVienContact(string maSV, string sdt, string email)
        {
            if (string.IsNullOrEmpty(maSV))
                return new Tuple<bool, string>(false, "Mã sinh viên không được để trống!");

            if (!IsValidPhoneNumber(sdt))
                return new Tuple<bool, string>(false, "Số điện thoại không hợp lệ!");

            if (!IsValidEmail(email))
                return new Tuple<bool, string>(false, "Địa chỉ email không hợp lệ!");

            if (GetSinhVienByMaSV(maSV) == null)
                return new Tuple<bool, string>(false, "Mã sinh viên không tồn tại!");

            bool result = SinhVienDAL.Instance.UpdateSinhVienContact(maSV, sdt, email);
            if (result)
                return new Tuple<bool, string>(true, "Cập nhật thông tin liên lạc thành công!");
            else
                return new Tuple<bool, string>(false, "Cập nhật thông tin liên lạc thất bại!");
        }
    }
}