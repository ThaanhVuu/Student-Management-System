using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace qlsv_dang_nhap.View
{
    /// <summary>
    /// Interaction logic for viewAdmin.xaml
    /// </summary>
    public partial class viewAdmin : Window
    {
        string connectionString = ConfigurationManager.ConnectionStrings["SMS"].ConnectionString;
        private AdmissionService _admissionService;
        private ProgramService _programService;
        string keyword = "";
        public viewAdmin()
        {
            InitializeComponent();
            var admissionRepository = new AdmissionRepository(connectionString);
            var programRepository = new ProgramRepository(connectionString);
            _admissionService = new AdmissionService(admissionRepository);
            _programService = new ProgramService(programRepository);
        }

        private void tabSelection(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Kiểm tra sender có phải là TabControl không
                if (!(sender is TabControl tabControl))
                {
                    return; // Không xử lý nếu sender không phải TabControl
                }

                // Kiểm tra SelectedItem có phải là TabItem không
                if (!(tabControl.SelectedItem is TabItem selectedTab))
                {
                    return; // Không xử lý nếu không có tab được chọn
                }

                // So sánh tên tab để tránh sai sót
                if (selectedTab.Name == nameof(AdmissionTabb)) // Kiểm tra tên tab "AdmissionTabb"
                {
                    LoadData();
                }
                else if (selectedTab.Name == nameof(ProgramTabb)) // Kiểm tra tên tab "ProgramTabb"
                {
                    LoadDataProgram();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chuyển tab: {ex.Message}");
            }
        }

        // Phương thức để tải dữ liệu ban đầu
        private void LoadData()
        {
            try
            {
                // Đảm bảo service đã được khởi tạo từ constructor
                DataTable newDt = string.IsNullOrEmpty(keyword)
                    ? _admissionService.GetAllAdmissions()
                    : _admissionService.SearchAdmission(keyword);

                // Kiểm tra dữ liệu trả về
                if (newDt == null)
                {
                    MessageBox.Show("Lỗi: Danh sách trả về null!");
                    lvAdmission.ItemsSource = null;
                    return;
                }

                if (newDt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu!");
                    lvAdmission.ItemsSource = null;
                    return;
                }

                lvAdmission.ItemsSource = newDt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách: {ex.Message}");
            }
        }

        private void LoadDataProgram()
        {
            try
            {
                DataTable newDt;
                newDt = _programService.GetAllPrograms();
                // Gán lại ItemsSource để kích hoạt cập nhật UI
                lvCTDT.ItemsSource = newDt.DefaultView;
                if (newDt?.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy kết quả");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách CTDT: {ex.Message}");
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


        private void ResetForm()
        {
            txtHoTen.Text = string.Empty;
            txtTenCTDT.Text = string.Empty;
            dpNgaySinh.SelectedDate = null;
            cbGioiTinh.SelectedIndex = -1;
        }

        // Xử lý các nút trong tab Quản lý tuyển sinh
        private void btnThemHoSo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //định dạng lại dữ liều đầu vào để phù hợp với db
                string ngaysinh = dpNgaySinh.SelectedDate?.ToString("yyyy-MM-dd");
                long abc = long.Parse(txtTenCTDT.Text);

                var admission = new Admission
                {
                    ProgramId = abc,
                    FullName = txtHoTen.Text.Trim(),
                    DOB = ngaysinh,
                    Gender = ((ComboBoxItem)cbGioiTinh.SelectedItem)?.Content.ToString(),
                    StatusAdmission = "Pending"
                };

                // Gọi service
                _admissionService.RegisterAdmission(admission);
                MessageBox.Show("Đã thêm hồ sơ thành công!");
                LoadData(); //load lai bang?
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm hồ sơ: {ex.Message}");
            }
        }

        // Đẩy dữ liệu từ DataGrid lên các box
        private void lvAdmission_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Lấy hàng được chọn
            var selectedRow = lvAdmission.SelectedItem as DataRowView;
            if (selectedRow == null) return;

            // Gán giá trị vào các controls
            try
            {
                // TextBox Tên CTDT và Họ tên
                txtTenCTDT.Text = selectedRow["program_id"].ToString();
                txtHoTen.Text = selectedRow["full_name"].ToString();

                // DatePicker Ngày sinh
                if (selectedRow["date_of_birth"] != DBNull.Value)
                {
                    dpNgaySinh.SelectedDate = Convert.ToDateTime(selectedRow["date_of_birth"]);
                }

                // ComboBox Giới tính
                string gender = selectedRow["gender"].ToString();
                foreach (ComboBoxItem item in cbGioiTinh.Items)
                {
                    if (item.Content.ToString() == gender)
                    {
                        cbGioiTinh.SelectedItem = item;
                        break;
                    }
                }

                // ComboBox Trạng thái
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}");
            }
        }

        private void btnSuaHoSo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Kiểm tra hàng được chọn
                var selectedRow = lvAdmission.SelectedItem as DataRowView;
                if (selectedRow == null)
                {
                    MessageBox.Show("Vui lòng chọn một hàng!");
                    return;
                }

                // Validate input
                string ngaysinh = dpNgaySinh.SelectedDate?.ToString("yyyy-MM-dd");
                long abc = long.Parse(txtTenCTDT.Text);
                long id2 = Convert.ToInt64(selectedRow["admission_id"]);


                // Tạo đối tượng Admission
                var admission = new Admission
                {
                    AdmissionId = id2,
                    ProgramId = abc,
                    FullName = txtHoTen.Text.Trim(),
                    DOB = ngaysinh,
                    Gender = ((ComboBoxItem)cbGioiTinh.SelectedItem)?.Content.ToString(),
                };

                // Gọi service cập nhật
                _admissionService.UpdateAdmission(admission);
                MessageBox.Show("Cập nhật thành công!");
                // Làm mới DataGrid
                LoadData();
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa hồ sơ: {ex.Message}");
            }
        }

        private void btnXoaHoSo_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = lvAdmission.SelectedItem as DataRowView;
            if (selectedRow == null)
            {
                MessageBox.Show("Vui lòng chọn hồ sơ!");
                return;
            }
            long id2 = Convert.ToInt64(selectedRow["admission_id"]); //validate id
            // TODO: Xóa hồ sơ tuyển sinh
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hồ sơ này?", "Xác nhận",
                                                    MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                // TODO: Thực hiện xóa dữ liệu
                try
                {
                    _admissionService.DeleteAdmission(id2);
                    MessageBox.Show("Đã xóa hồ sơ thành công!");
                    LoadData();
                    ResetForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa hồ sơ: {ex.Message}");
                }
            }
        }

        private void btnDuyetHoSo_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Duyệt hồ sơ tuyển sinh
            var selectedRow = lvAdmission.SelectedItem as DataRowView;
            if (selectedRow == null)
            {
                MessageBox.Show("Vui lòng chọn hồ sơ!");
                return;
            }
            long idd = Convert.ToInt64(selectedRow["admission_id"]);
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn duyệt hồ sơ này?", "Xác nhận",
                                                    MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _admissionService.ApproveAdmission(idd);

                MessageBox.Show("Đã duyệt hồ sơ!");
                LoadData();
                ResetForm();
            }

        }

        private void btnTuChoiHoSo_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Từ chối hồ sơ tuyển sinh
            var selectedRow = lvAdmission.SelectedItem as DataRowView;
            if (selectedRow == null)
            {
                MessageBox.Show("Vui lòng chọn hồ sơ!");
                return;
            }
            long idd = Convert.ToInt64(selectedRow["admission_id"]);
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn từ chỗi hồ sơ này?", "Xác nhận",
                                                    MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _admissionService.ApproveAdmission(idd);
                MessageBox.Show("Đã từ chối hồ sơ!");
                LoadData();
                ResetForm();
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
        private void ResetFormCTDT()
        {
            mct.Text = string.Empty;
            tct.Text = string.Empty;
        }
        private void btnThemCTDT_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Thêm mới chương trình đào tạo
            try
            {
                _programService.RegisterProgram(new Program
                {
                    ProgramID = int.Parse(mct.Text),
                    ProgramName = tct.Text
                });
                MessageBox.Show("Đã thêm chương trình đào tạo thành công!");
                LoadDataProgram();
                ResetFormCTDT();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm chương trình đào tạo: {ex.Message}");
            }
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
