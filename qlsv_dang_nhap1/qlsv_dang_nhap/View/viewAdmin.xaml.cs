using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Data;
using System.Configuration;

namespace qlsv_dang_nhap.View
{
    /// <summary>
    /// Interaction logic for viewAdmin.xaml
    /// </summary>
    public partial class viewAdmin : Window
    {
        string connectionString = ConfigurationManager.ConnectionStrings["SMS"].ConnectionString;
        private AdmissionSerVice _admissionService;
        string keyword;
        DataTable dt;
        public viewAdmin()
        {
            AdmissionRepository _admissionRepository = new AdmissionRepository(connectionString);
            _admissionService = new AdmissionSerVice(_admissionRepository);
            InitializeComponent();
            LoadData();

        }

        // Phương thức để tải dữ liệu ban đầu
        private void LoadData()
        {
            try
            {
                DataTable newDt;
                if (string.IsNullOrEmpty(keyword))
                {
                    newDt = _admissionService.GetAllAdmissions();
                }
                else
                {
                    newDt = _admissionService.SearchAdmission(keyword);
                }

                // Gán lại ItemsSource để kích hoạt cập nhật UI
                lvAdmission.ItemsSource = newDt?.DefaultView;

                if (newDt?.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy kết quả");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        #region Common Search Controls
        // Quản lý sinh viên
        private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            TextBlock placeholder = null;

            if (textBox == txtSearchStudent)
                placeholder = txtPlaceholderStudent;
            else if (textBox == txtSearchTuyenSinh)
                placeholder = txtPlaceholderTuyenSinh;
            else if (textBox == txtSearchCTDT)
                placeholder = txtPlaceholderCTDT;

            if (placeholder != null)
                placeholder.Visibility = Visibility.Collapsed;
        }

        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            TextBlock placeholder = null;

            if (textBox == txtSearchStudent)
                placeholder = txtPlaceholderStudent;
            else if (textBox == txtSearchTuyenSinh)
                placeholder = txtPlaceholderTuyenSinh;
            else if (textBox == txtSearchCTDT)
                placeholder = txtPlaceholderCTDT;

            if (placeholder != null && string.IsNullOrEmpty(textBox.Text))
                placeholder.Visibility = Visibility.Visible;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            TextBlock placeholder = null;

            if (textBox == txtSearchStudent)
                placeholder = txtPlaceholderStudent;
            else if (textBox == txtSearchTuyenSinh)
                placeholder = txtPlaceholderTuyenSinh;
            else if (textBox == txtSearchCTDT)
                placeholder = txtPlaceholderCTDT;

            if (placeholder != null)
                placeholder.Visibility = string.IsNullOrEmpty(textBox.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void btnSearchIcon_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string searchText = string.Empty;

            if (button == btnSearchIconStudent)
                searchText = txtSearchStudent.Text;
            else if (button == btnSearchIconTuyenSinh)
                searchText = txtSearchTuyenSinh.Text;
            else if (button == btnSearchIconCTDT)
                searchText = txtSearchCTDT.Text;

            // TODO: Thực hiện tìm kiếm dựa trên searchText và tab hiện tại
            MessageBox.Show($"Đang tìm kiếm: {searchText}");
        }
        #endregion

        #region Tab Quản lý sinh viên
        // Xử lý các nút trong tab Quản lý sinh viên
        private void btnThemSinhVien_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Thêm mới sinh viên
            MessageBox.Show("Chức năng thêm sinh viên được chọn.");
        }

        private void btnSuaSinhVien_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Sửa thông tin sinh viên
            MessageBox.Show("Chức năng sửa sinh viên được chọn.");
        }

        private void btnXoaSinhVien_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Xóa sinh viên
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này?", "Xác nhận",
                                                    MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                // TODO: Thực hiện xóa dữ liệu
                MessageBox.Show("Đã xóa sinh viên thành công!");
            }
        }
        #endregion

        #region Tab Quản lý tuyển sinh
        // Xử lý các nút trong tab Quản lý tuyển sinh
        private void btnThemHoSo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Console.WriteLine($"Debug - SelectedDate: {dob.SelectedDate}");
                // Kiểm tra dữ liệu
                if (string.IsNullOrEmpty(txtHoTen.Text))
                {
                    MessageBox.Show("Họ tên không được để trống");
                    return;
                }
                if (string.IsNullOrEmpty(txtTenCTDT.Text))
                {
                    MessageBox.Show("Tên chương trình đào tạo không được để trống");
                    return;
                }
                if (!dob.SelectedDate.HasValue) // ✅ Đã sửa ở đây
                {
                    MessageBox.Show("Ngày sinh không được để trống");
                    return;
                }
                if (cbGioiTinh.SelectedIndex == -1)
                {
                    MessageBox.Show("Giới tính không được để trống");
                    return;
                }
                if (cbTrangThai.SelectedIndex == -1)
                {
                    MessageBox.Show("Trạng thái không được để trống");
                    return;
                }

                // Xử lý ngày sinh (đã đảm bảo SelectedDate != null)
                DateOnly ngaysinh = DateOnly.FromDateTime(dob.SelectedDate.Value);

                // Tạo đối tượng (không gán AdmissionId)
                var admission = new Admission
                {
                    FullName = txtHoTen.Text.Trim(),
                    DOB = ngaysinh,
                    Gender = cbGioiTinh.SelectedItem?.ToString() ?? "",
                    StatusAdmission = cbTrangThai.SelectedItem?.ToString() ?? ""
                };

                // Gọi service
                _admissionService.RegisterAdmission(admission);

                MessageBox.Show("Đã thêm hồ sơ thành công!");
                LoadData();

                // Reset form
                txtHoTen.Text = string.Empty;
                txtTenCTDT.Text = string.Empty; // Thêm dòng này nếu cần
                dob.SelectedDate = null;
                dob.Text = string.Empty; // ⭐ Thêm dòng này để clear hiển thị
                cbGioiTinh.SelectedIndex = -1;
                cbTrangThai.SelectedIndex = -1;
                // Sau khi reset form
                dob.UpdateLayout(); // ⭐ Cập nhật layout cho DatePicker
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void btnSuaHoSo_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Sửa thông tin hồ sơ tuyển sinh
            MessageBox.Show("Chức năng sửa hồ sơ tuyển sinh được chọn.");
        }

        private void btnXoaHoSo_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Xóa hồ sơ tuyển sinh
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hồ sơ này?", "Xác nhận",
                                                    MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                // TODO: Thực hiện xóa dữ liệu
                MessageBox.Show("Đã xóa hồ sơ thành công!");
            }
        }

        private void btnDuyetHoSo_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Duyệt hồ sơ tuyển sinh
            MessageBox.Show("Đã duyệt hồ sơ thành công!");
        }

        private void btnTuChoiHoSo_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Từ chối hồ sơ tuyển sinh
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn từ chối hồ sơ này?", "Xác nhận",
                                                    MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                // TODO: Cập nhật trạng thái hồ sơ
                MessageBox.Show("Đã từ chối hồ sơ!");
            }
        }

        // Event handlers cho ô tìm kiếm

        private void txtSearchTuyenSinh_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Hiển thị hoặc ẩn placeholder dựa vào nội dung của TextBox
            txtPlaceholderTuyenSinh.Visibility = string.IsNullOrEmpty(txtSearchTuyenSinh.Text) ?
                Visibility.Visible : Visibility.Collapsed;

            // TODO: Thêm mã tìm kiếm của bạn ở đây
         
        }

        private void k_search(object sender, KeyEventArgs e)
        {
            // Ẩn placeholder khi TextBox được focus
            if (e.Key == Key.Enter)
            {
                // Xử lý sự kiện khi nhấn nút tìm kiếm
                btnSearchIconTuyenSinh_Click(sender, e);
            }
        }

        private void PerformSearch()
        {
            keyword = txtSearchTuyenSinh.Text.Trim();
            LoadData();
        }

        private void txtSearchTuyenSinh_GotFocus(object sender, RoutedEventArgs e)
        {
            // Ẩn placeholder khi TextBox được focus
            txtPlaceholderTuyenSinh.Visibility = Visibility.Collapsed;
        }

        private void txtSearchTuyenSinh_LostFocus(object sender, RoutedEventArgs e)
        {
            // Hiển thị placeholder khi TextBox mất focus và không có nội dung
            if (string.IsNullOrEmpty(txtSearchTuyenSinh.Text))
            {
                txtPlaceholderTuyenSinh.Visibility = Visibility.Visible;
            }
        }

        private void btnSearchIconTuyenSinh_Click(object sender, RoutedEventArgs e)
        {
            // Xử lý sự kiện khi nhấn nút tìm kiếm
            // TODO: Thêm mã xử lý tìm kiếm của bạn ở đây
            PerformSearch();
        }
        #endregion

        #region Tab Chương trình đào tạo
        private void btnThemCTDT_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Thêm mới chương trình đào tạo
            MessageBox.Show("Chức năng thêm CTĐT được chọn.");
        }

        private void btnSuaCTDT_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Sửa thông tin chương trình đào tạo
            MessageBox.Show("Chức năng sửa CTĐT được chọn.");
        }

        private void btnXoaCTDT_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Xóa chương trình đào tạo
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa chương trình đào tạo này?", "Xác nhận",
                                                    MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                // TODO: Thực hiện xóa dữ liệu
                MessageBox.Show("Đã xóa CTĐT thành công!");
            }
        }

        private void btnChiTiet_Click(object sender, RoutedEventArgs e)
        {
            // Sử dụng trực tiếp TabControlMain đã đặt tên thay vì FindName
            if (TabControlMain != null)
            {
                TabControlMain.SelectedIndex = 3; // Chuyển đến tab Chi tiết CTĐT (index 3)
            }
        }

        //private void btnChiTiet_Click(object sender, RoutedEventArgs e)
        //{
        //    // TODO: Chuyển đến tab Chi tiết CTĐT
        //    // Ví dụ chuyển đổi sang tab thứ 4 (index 3) là tab Chi tiết CTĐT
        //    TabControl tabControl = this.FindName("TabControlMain") as TabControl;
        //    if (tabControl != null)
        //    {
        //        tabControl.SelectedIndex = 3;
        //    }
        //}
        #endregion

        #region Tab Chi tiết CTĐT
        private void btnThemMonHoc_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Thêm môn học vào CTĐT
            MessageBox.Show("Chức năng thêm môn học vào CTĐT được chọn.");
        }

        private void btnXoaMonHoc_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Xóa môn học khỏi CTĐT
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa môn học này khỏi CTĐT?", "Xác nhận",
                                                    MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                // TODO: Thực hiện xóa dữ liệu
                MessageBox.Show("Đã xóa môn học khỏi CTĐT thành công!");
            }
        }
        #endregion

        #region Tab Môn học
        private void btnThemMH_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Thêm mới môn học
            MessageBox.Show("Chức năng thêm môn học được chọn.");
        }

        private void btnSuaMH_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Sửa thông tin môn học
            MessageBox.Show("Chức năng sửa môn học được chọn.");
        }

        private void btnXoaMH_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Xóa môn học
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa môn học này?", "Xác nhận",
                                                    MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                // TODO: Thực hiện xóa dữ liệu
                MessageBox.Show("Đã xóa môn học thành công!");
            }
        }

        private void lvMonHoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO: Hiển thị thông tin môn học được chọn
        }
        #endregion

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Xử lý sự kiện khi đóng cửa sổ
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận",
                                                    MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
