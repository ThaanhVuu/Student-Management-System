using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace qlsv_dang_nhap.userControl
{
    /// <summary>
    /// Interaction logic for Trang_chinh_Control.xaml
    /// </summary>
    public partial class Trang_chinh_Control : UserControl
    {
        public Trang_chinh_Control()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = e.Uri.AbsoluteUri,
                UseShellExecute = true
            };
            Process.Start(psi);
            e.Handled = true;
        }
        //video
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            videoPlayer.Play(); // Tự động chạy video khi mở cửa sổ
        }
    }

}
