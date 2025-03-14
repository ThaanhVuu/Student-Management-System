using System.ComponentModel;

    public class StudentViewModel : INotifyPropertyChanged
    {
        private string _maSV;
        private string _hoTen;
        private string _ngaySinh;
        private string _gioiTinh;
        private string _chuyenNganh;
        private string _dienThoai;
        private string _email;

        public string MaSV
        {
            get => _maSV;
            set { _maSV = value; OnPropertyChanged(nameof(MaSV)); }
        }

        public string HoTen
        {
            get => _hoTen;
            set { _hoTen = value; OnPropertyChanged(nameof(HoTen)); }
        }

        public string NgaySinh
        {
            get => _ngaySinh;
            set { _ngaySinh = value; OnPropertyChanged(nameof(NgaySinh)); }
        }

        public string GioiTinh
        {
            get => _gioiTinh;
            set { _gioiTinh = value; OnPropertyChanged(nameof(GioiTinh)); }
        }

        public string ChuyenNganh
        {
            get => _chuyenNganh;
            set { _chuyenNganh = value; OnPropertyChanged(nameof(ChuyenNganh)); }
        }

        public string DienThoai
        {
            get => _dienThoai;
            set { _dienThoai = value; OnPropertyChanged(nameof(DienThoai)); }
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(nameof(Email)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
