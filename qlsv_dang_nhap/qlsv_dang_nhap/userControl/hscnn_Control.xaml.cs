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
using qlsv_dang_nhap.srcMVC.controller;
using qlsv_dang_nhap.userControl;
using qlsv_dang_nhap.viewmodel;

namespace qlsv_dang_nhap.userControl
{
    /// <summary>
    /// Interaction logic for hscnn_Control.xaml
    /// </summary>
    public partial class hscnn_Control : UserControl
    {
        public StudentViewModel ViewModel { get; set; }

        public hscnn_Control(Student student)
        {
            InitializeComponent();
            ViewModel = new StudentViewModel
            {
                MaSV = student.MaSV,
                HoTen = student.HoTen,
                NgaySinh = student.NgaySinh,
                GioiTinh = student.GioiTinh,
                ChuyenNganh = student.ChuyenNganh,
                DienThoai = student.DienThoai,
                Email = student.Email
            };
            DataContext = ViewModel;
        }
    }
}
