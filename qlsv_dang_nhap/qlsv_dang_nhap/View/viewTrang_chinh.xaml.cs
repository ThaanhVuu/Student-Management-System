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
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;
using System.Diagnostics;
using System.Windows.Navigation;

namespace qlsv_dang_nhap.View
{
    /// <summary>
    /// Interaction logic for viewTrang_chinh.xaml
    /// </summary>
    public partial class viewTrang_chinh : Window
    {
        private Popup currentPopup;
        public viewTrang_chinh()
        {
            InitializeComponent();
            ContentDisplay.Content = new qlsv_dang_nhap.userControl.Trang_chinh_Control();
            this.Loaded += StartTextAnimation;
            this.SizeChanged += UpdateAnimation;

            // Gán sự kiện 
            chuong_trinh_dao_tao.Click += Button_Click;
            dang_ki.Click += Button_Click;
            thong_tin_ca_nhan.Click += Button_Click;
            danh_gia.Click += Button_Click;
            tai_chinh.Click += Button_Click;
            dich_vu.Click += Button_Click;

        }
        //hyperlink
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
        //mouse_effect
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        //button mini_size
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        //button max_size
        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
                btnMaximize.Content = "🗗";
            }
            else
            {
                this.WindowState = WindowState.Normal;
                btnMaximize.Content = "⬜";
            }
        }
        //button close
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //thanh search
        private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPlaceholder.Visibility = Visibility.Collapsed;
        }
        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                txtPlaceholder.Visibility = Visibility.Visible;
            }
        }
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtPlaceholder.Visibility = string.IsNullOrEmpty(txtSearch.Text) ? Visibility.Visible : Visibility.Collapsed;
        }
        private void btnSearchIcon_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chức năng tìm kiếm chưa được triển khai.");
        }
        //ham di chuyen text
        private void StartTextAnimation(object sender, RoutedEventArgs e)
        {
            AnimateText();
        }

        private void UpdateAnimation(object sender, SizeChangedEventArgs e)
        {
            AnimateText();
        }

        private void AnimateText()
        {
            if (borderContainer.ActualWidth == 0) return;

            double fromX = borderContainer.ActualWidth;
            double toX = -movingText.ActualWidth;

            DoubleAnimation animation = new DoubleAnimation
            {
                From = fromX,
                To = toX,
                Duration = new Duration(TimeSpan.FromSeconds(8)),
                RepeatBehavior = RepeatBehavior.Forever
            };

            textTransform.BeginAnimation(TranslateTransform.XProperty, animation);
        }
        //popup
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                CloseCurrentMenu();
                if (currentPopup == null || !currentPopup.IsOpen)
                {
                    currentPopup = FindPopupForButton(btn);
                    if (currentPopup != null)
                    {
                        currentPopup.IsOpen = true;
                    }
                }
            }
        }
        private Popup FindPopupForButton(Button button)
        {
            return button.Name switch
            {
                "chuong_trinh_dao_tao" => menuPopup_CTDT,
                "dang_ki" => menuPopup_DK,
                "thong_tin_ca_nhan" => menuPopup_TTCN,
                "danh_gia" => menuPopup_DG,
                "tai_chinh" => menuPopup_TC,
                "dich_vu" => menuPopup_DV,
                _ => null
            };
        }
        private void CloseCurrentMenu()
        {
            if (currentPopup != null)
            {
                currentPopup.IsOpen = false;
                currentPopup = null;
            }
        }
        //buttonView
        private void trang_chu_click(object sender, RoutedEventArgs e)
        {
         
        }

        private void TTCN_click(object sender, RoutedEventArgs e)
        {
           
        }
        private void kqht_click(object sender, RoutedEventArgs e)
        {
            ContentDisplay.Content = new qlsv_dang_nhap.userControl.kqhtControl();
        }
        private void dang_ky_click(object sender, RoutedEventArgs e)
        {
            ContentDisplay.Content = new qlsv_dang_nhap.userControl.dkControl();
        }
    }

}
