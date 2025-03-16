// StudentViewModel.cs
using System.ComponentModel;

public class StudentViewModel : INotifyPropertyChanged
{
    private string? _maSV = string.Empty;
    private string? _hoTen = string.Empty;
    private string? _ngaySinh = string.Empty;
    private string? _gioiTinh = string.Empty;
    private string? _nganh = string.Empty;
    private string? _sdt = string.Empty;
    private string? _email = string.Empty;

    public string? MaSV
    {
        get => _maSV;
        set { _maSV = value; OnPropertyChanged(nameof(MaSV)); }
    }

    public string? HoTen
    {
        get => _hoTen;
        set { _hoTen = value; OnPropertyChanged(nameof(HoTen)); }
    }

    public string? NgaySinh
    {
        get => _ngaySinh;
        set { _ngaySinh = value; OnPropertyChanged(nameof(NgaySinh)); }
    }

    public string? GioiTinh
    {
        get => _gioiTinh;
        set { _gioiTinh = value; OnPropertyChanged(nameof(GioiTinh)); }
    }

    public string? Nganh
    {
        get => _nganh;
        set { _nganh = value; OnPropertyChanged(nameof(Nganh)); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}