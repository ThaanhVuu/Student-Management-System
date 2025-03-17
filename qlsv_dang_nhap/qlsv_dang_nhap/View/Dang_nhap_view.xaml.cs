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
using System.Configuration;
using qlsv_dang_nhap.userControl;
using qlsv_dang_nhap.srcMVC;

namespace qlsv_dang_nhap.View
{
    /// <summary>
    /// Interaction logic for Dang_nhap_view.xaml
    /// </summary>
    /// 
    public partial class Dang_nhap_view : Window
    {
        string connectionString = ConfigurationManager.ConnectionStrings["SMS"].ConnectionString;
        private User _user;
        private UserService _userService;

        public Dang_nhap_view()
        {
            _user = new User();
            var _userRepository = new UserRepository(connectionString);
            _userService = new UserService(_userRepository);
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string userInput = txtUser.Text;
            string passInput = txtPass.Password;
            if (string.IsNullOrEmpty(userInput) || string.IsNullOrEmpty(passInput))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu");
                return;
            }
            try
            {
                _user = _userService.Authenticate(userInput, passInput);
                //MessageBox.Show($"Đăng nhập thành công, chào mừng {_user.Username}");
                if (_user.Role == 1)
                {
                    viewAdmin admin = new viewAdmin();
                    admin.Show();
                    this.Close();
                }
                else
                {
                    StudentMVC.LoggedInMaSV = userInput; // maSV là mã sinh viên đăng nhập thành công
                    viewTrang_chinh user = new viewTrang_chinh();
                    user.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

            private void mk_enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }

        private void btnquenmatkhau_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Vui lòng liên hệ Admin để được cấp lại mật khẩu!");
            return;
        }
    }
}
