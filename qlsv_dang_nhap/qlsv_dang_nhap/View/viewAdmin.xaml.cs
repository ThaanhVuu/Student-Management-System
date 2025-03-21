﻿using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace qlsv_dang_nhap.View
{
    /// <summary> ///da xong conflict
    /// Interaction logic for viewAdmin.xaml
    /// </summary>
    public partial class viewAdmin : Window
    {
        #region init
        private AdmissionService _admissionService;
        private ProgramService _programService;
        private StudentService _studentService;
        private CourseService _courseService;
        private CourseProgramService _CPService;
        string keyword;
        public viewAdmin()
        {
            InitializeComponent();
            _admissionService = new AdmissionService();
            _programService = new ProgramService();
            _studentService = new StudentService();
            _courseService = new CourseService();
            _CPService = new CourseProgramService();
        }
        //load dung ds
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
                else if (selectedTab.Name == nameof(StudentTabb))
                {
                    LoadDataStudent();
                }else if(selectedTab.Name == nameof(CourseTabb))
                {
                    LoadCourse();
                }else if (selectedTab.Name == nameof(CPTabb))
                {
                    loadMiniList();
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

                lvAdmission.ItemsSource = newDt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách: {ex.Message}");
            }
            txtMaHoSo.IsReadOnly = true;
        }

        private void LoadDataProgram()
        {
            try
            {
                DataTable newDt;
                newDt = _programService.GetAllPrograms();
                // Gán lại ItemsSource để kích hoạt cập nhật UI
                lvCTDT.ItemsSource = newDt.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách CTDT: {ex.Message}");
            }
        }

        private void LoadDataStudent()
        {
            try
            {
                DataTable dt;
                dt = string.IsNullOrEmpty(keyword) ? _studentService.getAllStudent() : _studentService.SearchStudent(keyword);
                dssv.ItemsSource = dt.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách sinh viên: {ex.Message}");
            }
        }

        private void LoadCourse()
        {
            var dt = _courseService.getCourse();
            lvMonHoc.ItemsSource = dt.DefaultView;
            courseId.IsEnabled = false;
            //courseId.IsReadOnly = true;
            //lvMonHoc.ReadOnly = false;
            //lvMonHoc.AllowUserToAddRows = true;
            //lvMonHoc.AllowUserToDeleteRows = true;
            //lvMonHoc.EditMode = DataGridEditMode.EditOnEnter;
        }
        #endregion

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

        private void StudentResetForm()
        {
            hoten.Text = string.Empty;
            dob.Text = string.Empty;
            gioitinh.SelectedIndex = -1;
            cbProgram.SelectedIndex = -1;
            lop.Text = string.Empty;
            trangthai.SelectedIndex = -1;
        }

        private void dssvSelection(object sender, SelectionChangedEventArgs e)
        {
            // Lấy hàng được chọn
            var selectedRow = dssv.SelectedItem as DataRowView;
            if (selectedRow == null) return;

            // Gán giá trị vào các controls
            try
            {
                msv.Text = selectedRow["admission_id"].ToString();
                hoten.Text = selectedRow["full_name"].ToString();
                dob.Text = ((DateTime)selectedRow["date_of_birth"]).ToString("yyyy-MM-dd");
                string gender = selectedRow["gender"].ToString();
                foreach (ComboBoxItem item in gioitinh.Items)
                {
                    if (item.Content.ToString() == gender)
                    {
                        gioitinh.SelectedItem = item;
                        break;
                    }
                }
                string program = selectedRow["program_id"].ToString();
                foreach (ComboBoxItem item in cbProgram.Items)
                {
                    if (item.Content.ToString() == program)
                    {
                        cbProgram.SelectedItem = item;
                        break;
                    }
                }
                lop.Text = selectedRow["class_name"].ToString();
                string status = selectedRow["student_status"].ToString();
                foreach (ComboBoxItem item in trangthai.Items)
                {
                    if (item.Content.ToString() == status)
                    {
                        trangthai.SelectedItem = item;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn sinh viên: {ex.Message}");
            }
        }

        private void btnThemSinhVien_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                long pid = long.Parse(((ComboBoxItem)cbProgram.SelectedItem).Content.ToString());

                // Tạo và thêm Admission
                var admission = new Admission
                {
                    ProgramId = pid,
                    FullName = hoten.Text,
                    DOB = dob.Text,
                    Gender = ((ComboBoxItem)gioitinh.SelectedItem).Content.ToString(),
                    StatusAdmission = "Approved"
                };

                long admissionID = _admissionService.RegisterAdmission(admission); // Phương thức đã sửa

                // Tạo Student từ thông tin Admission
                var student = new Student
                {
                    Id = (long)admissionID,
                    Name = admission.FullName,
                    Dob = admission.DOB,
                    gender = admission.Gender,
                    ProgramId = (long)admission.ProgramId,
                    ClassName = lop.Text,
                    Status = ((ComboBoxItem)trangthai.SelectedItem).Content.ToString()
                };

                _studentService.AddStudent(student);
                MessageBox.Show("Thêm hồ sơ sinh viên thành công!");
                LoadDataStudent(); StudentResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm sinh viên: {ex.Message}");
            }
        }

        private void btnSuaSinhVien_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                // Lấy giá trị gender từ ComboBox
                string gender = ((ComboBoxItem)gioitinh.SelectedItem).Content.ToString();

                Student student = new Student
                {
                    Id = long.Parse(msv.Text),
                    Name = hoten.Text,
                    Dob = dob.Text,
                    gender = gender, // Sử dụng giá trị đã kiểm tra
                    ProgramId = long.Parse(((ComboBoxItem)cbProgram.SelectedItem).Content.ToString()),
                    ClassName = lop.Text,
                    Status = ((ComboBoxItem)trangthai.SelectedItem).Content.ToString(),
                };
                _studentService.UpdateStudent(student);

                Admission admission = new Admission
                {
                    AdmissionId = long.Parse(msv.Text),
                    FullName = hoten.Text,
                    DOB = dob.Text,
                    Gender = gender, // Sử dụng giá trị đã kiểm tra
                    ProgramId = long.Parse(((ComboBoxItem)cbProgram.SelectedItem).Content.ToString()),
                };
                _admissionService.UpdateAdmission(admission);

                MessageBox.Show("Sửa hồ sơ thành công!");
                LoadDataStudent();
                StudentResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa hồ sơ: {ex.ToString()}"); // Hiển thị chi tiết lỗi
            }
        }

        private void k_studentsearch(object sender, KeyEventArgs e)
        {
            keyword = txtSearchStudent.Text.Trim();
            if (e.Key == Key.Enter)
            {
                LoadDataStudent();
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
                txtMaHoSo.Text = selectedRow["admission_id"].ToString();
                // TextBox Tên CTDT và Họ tên
                txtTenCTDT.Text = selectedRow["program_id"].ToString();
                txtHoTen.Text = selectedRow["full_name"].ToString();
                sts.Text = selectedRow["admission_status"].ToString();

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
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu Hồ sơ tuyển sinh: {ex.Message}");
            }

        }

        private void btnSuaHoSo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate input
                string ngaysinh = dpNgaySinh.SelectedDate?.ToString("yyyy-MM-dd");
                long abc = long.Parse(txtTenCTDT.Text);
                long id2 = long.Parse(txtMaHoSo.Text);


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
            long id2 = 0;
            if (id2 == 0)
            {
                MessageBox.Show("Vui lòng chọn hồ sơ cần xóa"); return;
            }
            id2 = long.Parse(txtMaHoSo.Text);
            // TODO: Xóa hồ sơ tuyển sinh
            MessageBoxResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa hồ sơ tuyển sinh và hồ sơ sinh viên {id2} ?", "Xác nhận",
                                                    MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                // TODO: Thực hiện xóa dữ liệu
                try
                {
                    Student s = new Student();
                    s.Id = id2;
                    _studentService.DeleteStudent(s);
                    _admissionService.DeleteAdmission(id2);
                    MessageBox.Show("Đã xóa hồ sơ thành công!");
                    LoadData();
                    ResetForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa hồ sơ tuyển sinh(sinh viên): {ex.Message}");
                }
            }
        }

        private void btnDuyetHoSo_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Duyệt hồ sơ tuyển sinh
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn duyệt hồ sơ này?", "Xác nhận",
                                                    MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var selectedRow = lvAdmission.SelectedItem as DataRowView;
                    // Tạo đối tượng Admission
                    var admission = new Admission
                    {
                        AdmissionId = long.Parse(txtMaHoSo.Text),
                        ProgramId = long.Parse(txtTenCTDT.Text),
                        FullName = txtHoTen.Text.Trim(),
                        DOB = dpNgaySinh.SelectedDate?.ToString("yyyy-MM-dd"),
                        Gender = ((ComboBoxItem)cbGioiTinh.SelectedItem)?.Content.ToString(),
                        StatusAdmission = sts.Text
                    };
                    _admissionService.ApproveAdmission(admission);
                    MessageBox.Show("Đã duyệt hồ sơ!");
                    LoadData();
                    ResetForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi duyệt hồ sơ: " + ex.Message);
                }

            }

        }

        private void btnTuChoiHoSo_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = lvAdmission.SelectedItem as DataRowView;
            // TODO: Từ chối hồ sơ tuyển sinh
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn từ chỗi hồ sơ này?", "Xác nhận",
                                                    MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Admission admission = new Admission
                    {
                        AdmissionId = long.Parse(txtMaHoSo.Text),
                        StatusAdmission = sts.Text
                    };
                    _admissionService.RejectAdmission(admission);
                    MessageBox.Show("Đã từ chối hồ sơ");
                    LoadData();
                    ResetForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi từ chối hồ sơ: " + ex.Message);
                }
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

        private string _originalProgramId;
        private void lvCTDTSelected(object sender, SelectionChangedEventArgs e)
        {

            var selectedRow = lvCTDT.SelectedItem as DataRowView;
            if (selectedRow == null) return;
            // Gán giá trị vào các controls
            try
            {
                // TextBox Mã CTDT và Tên CTDT
                _originalProgramId = selectedRow["program_id"].ToString();
                mct.Text = selectedRow["program_id"].ToString();
                tct.Text = selectedRow["program_name"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu CTDT: {ex.Message}");
            }
        }

        private void btnThemCTDT_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Thêm mới chương trình đào tạo
            if (string.IsNullOrEmpty(tct.Text))
            {
                MessageBox.Show("Vui lòng nhập tên chương trình đào tạo!");
                return;
            }
            else if (string.IsNullOrEmpty(mct.Text))
            {
                MessageBox.Show("Vui lòng nhập mã chương trình đào tạo!");
                return;
            }
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
            if (string.IsNullOrEmpty(mct.Text))
            {
                MessageBox.Show("Vui lòng chọn chương trình đào tạo cần sửa!");
                return;
            }
            try
            {
                if (tct.Text == _originalProgramId)
                {
                    _programService.UpdateProgram(new Program
                    {
                        ProgramID = long.Parse(mct.Text),
                        ProgramName = tct.Text
                    });
                    MessageBox.Show("Đã sửa chương trình đào tạo thành công!");
                    LoadDataProgram(); ResetFormCTDT();
                }
                else
                {
                    MessageBox.Show("Không thể sửa mã chương trình đào tạo!");
                    ResetFormCTDT();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa chương trình đào tạo: {ex.Message}");
            }
        }
        private void btnXoaCTDT_Click(object sender, RoutedEventArgs e)
        {
            // check xem chon row ch
            if (string.IsNullOrEmpty(mct.Text))
            {
                MessageBox.Show("Vui lòng chọn chương trình đào tạo cần xóa!");
                return;
            }

            // Xác nhận xóa
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa chương trình đào tạo này?", "Xác nhận",
                                                      MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                // Thực hiện xóa dữ liệu
                try
                {
                    Program program = new Program
                    {
                        ProgramID = long.Parse(mct.Text),
                        ProgramName = tct.Text
                    };
                    _programService.DeleteProgram(program);
                    LoadDataProgram(); ResetFormCTDT();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa chương trình đào tạo: {ex.Message}");
                }
            }
        }
        private void btnChiTiet_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(mct.Text))
            {
                MessageBox.Show("Vui lòng chọn CTDT");
            }
            else
            {
                // Sử dụng trực tiếp TabControlMain đã đặt tên thay vì FindName
                if (TabControlMain != null)
                {
                    TabControlMain.SelectedIndex = 3; // Chuyển đến tab Chi tiết CTĐT (index 3)
                    LoadCourseProgram();
                    tenCTDT.IsEnabled = false;
                    tenCTDT.Text = tct.Text;
                }
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
            try
            {
                _CPService.addCP(Convert.ToInt32(idMiniMonHoc), Convert.ToInt32(mct.Text));
                MessageBox.Show("Thêm môn học cho chương trình đào tạo thành công");
                LoadCourseProgram();  
            }catch(Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm môn học cho chương trình đào tạo: " + ex.Message);
            }

        }

        private void loadMiniList()
        {
            var dt = new DataTable();
            dt = _courseService.getCourse();
            miniMonHoc.ItemsSource = dt.DefaultView;
        }

        string idMiniMonHoc;
        private void miniMonHocc(object sender, SelectionChangedEventArgs e)
        {
            var selectedRow = miniMonHoc.SelectedItem as DataRowView;
            if (selectedRow == null) return;
            // Gán giá trị vào các controls
            try
            {
                idMiniMonHoc = selectedRow["course_id"].ToString(); //lay ma mon hoc o ds mon hoc mini
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn dòng môn học: {ex.Message}");
            }
        }

        private void LoadCourseProgram()
        {
            var cs = new CourseProgram();
            cs.program_id = Convert.ToInt64(mct.Text);
            var dt = new DataTable();
            dt = _CPService.getAllCourseProgram(cs);
            lvCP.ItemsSource = dt.DefaultView;
        }

        string iidd;
        private void lvCTDTSelected2(object sender, SelectionChangedEventArgs e)
        {
            var selected = lvCP.SelectedItem as DataRowView;
            if (selected == null) return;
            try
            {
                iidd = selected["course_id"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn môn học từ chương trình đào tạo: {ex.Message}");
            }
        }

        private void btnXoaMonHoc_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Xóa môn học khỏi CTĐT
            MessageBoxResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa môn học {iidd} khỏi CTĐT?", "Xác nhận",
                                                    MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                // TODO: Thực hiện xóa dữ liệu
                try {
                    //lay table chitiet ctdt
                    var selectedRow = lvCP.SelectedItem as DataRowView;
                    if (selectedRow == null) return;
                    iidd = selectedRow["course_id"].ToString();
                    int a = int.Parse(iidd);
                    _CPService.removeCP(a);
                    MessageBox.Show("Đã xóa môn học khỏi CTĐT thành công!");
                    LoadCourseProgram();

                }catch(Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa môn học trong CTDT {tenCTDT.Text}" + ex.Message);
                }
            }
        }
        #endregion

        #region Tab Môn học
        private void CourseResetForm()
        {
            txtNameCourse.Text = string.Empty;
            creditCourse.Text = string.Empty;
        }

        private void courseSelection(object sender, SelectionChangedEventArgs e)
        {
            var selectedRow = lvMonHoc.SelectedItem as DataRowView;
            if (selectedRow == null) return;
            try
            {
                txtNameCourse.Text = selectedRow.Row["course_name"].ToString();
                courseId.Text = selectedRow["course_id"].ToString();
                creditCourse.Text = selectedRow["credit"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn môn học"); 
            }
        }

        private void btnThemMH_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Kiểm tra dữ liệu hợp lệ
                if (string.IsNullOrWhiteSpace(txtNameCourse.Text) || !int.TryParse(creditCourse.Text, out int a))
                {
                    MessageBox.Show("Vui lòng điền đủ thông tin và số tín chỉ hợp lệ");
                    return;
                }

                // Xử lý thêm môn học
                var dt = _courseService.getCourse();
                _courseService.AddCourse(dt, txtNameCourse.Text, a);
                _courseService.saverCourse(dt);

                MessageBox.Show("Thêm môn học thành công");
                LoadCourse();
                CourseResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm môn học: {ex.Message}");
            }
        }

        private void btnSuaMH_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Sửa thông tin môn học
            try
            {
                var dt = _courseService.getCourse();
                _courseService.EditCourse(dt,Convert.ToInt32(courseId.Text),txtNameCourse.Text,Convert.ToInt32(creditCourse.Text));
                _courseService.saverCourse(dt);
                MessageBox.Show("Sửa môn học thành công");
                LoadCourse(); CourseResetForm() ;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa môn học" + ex.Message);
            }
        }

        private void btnXoaMH_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Xóa môn học
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa môn học này?", "Xác nhận",
                                                    MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                // TODO: Thực hiện xóa dữ liệu
                try
                {
                    var dt = _courseService.getCourse();
                    _courseService.DeleteCourse(dt, Convert.ToInt32(courseId.Text));
                    _courseService.saverCourse(dt);
                    MessageBox.Show("Đã xóa môn học thành công!");
                    LoadCourse(); ResetForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa môn học" + ex.Message);
                }
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
