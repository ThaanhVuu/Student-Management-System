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
using System.Windows.Navigation;
using System.Windows.Shapes;
using qlsv_dang_nhap.srcMVC;
using qlsv_dang_nhap.userControl;

namespace qlsv_dang_nhap.userControl
{
    /// <summary>
    /// Interaction logic for hscnn_Control.xaml
    /// </summary>
    public partial class hscnn_Control : UserControl
    {
        public StudentViewModel ViewModel { get; set; }

        public hscnn_Control(StudentMVC student)
        {
            try
            {
                InitializeComponent();
                ViewModel = new StudentViewModel
                {
                    MaSV = student.MaSV ?? "",
                    HoTen = student.HoTen ?? "",
                    NgaySinh = student.NgaySinh ?? "",
                    GioiTinh = student.GioiTinh ?? "",
                    Nganh = student.Nganh ?? "",
                };
                DataContext = ViewModel;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
            }
        }

        private void btnSuathongSinhien_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chức năng chưa được triển khai.");
        }

        private void btnXoathongtinSinhvien_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chức năng chưa được triển khai.");
        }
    }
}
