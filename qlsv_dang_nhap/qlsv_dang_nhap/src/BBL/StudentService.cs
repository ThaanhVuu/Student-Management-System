using System.Data;

class StudentService
{
    private readonly StudentRepository _studentRepository;
    public StudentService(StudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public DataTable getAllStudent()
    {
        return _studentRepository.GetStudents();
    }

    public void AddStudent(Student student)
    {
        if (student.Id == null)
        {
            throw new Exception("Vui lòng điền đủ thông tin hồ sơ");
        }
        if (student.Name == null)
        {
            throw new Exception("Vui lòng điền đủ thông tin hồ sơ");
        }
        if (student.gender == null)
        {
            throw new Exception("Vui lòng điền đủ thông tin hồ sơ");
        }
        if (student.Dob == null)
        {
            throw new Exception("Vui lòng điền đủ thông tin hồ sơ");
        }
        if (student.Status == null)
        {
            throw new Exception("Vui lòng điền đủ thông tin hồ sơ");
        }
        if (student.ClassName == null)
        {
            throw new Exception("Vui lòng điền đủ thông tin hồ sơ");
        }
        try
        {
            _studentRepository.AddStudent(student);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void UpdateStudent(Student student)
    {
        if (student.Id == null)
        {
            throw new Exception("Vui lòng chọn hồ sơ sinh viên cần cập nhật");
        }
        if (student.Name == null || student == null)
        {
            throw new Exception("Vui lòng điền đủ thông tin hồ sơ");
        }
        if (student.gender == null)
        {
            throw new Exception("Vui lòng điền đủ thông tin hồ sơ");
        }
        if(student.Dob == null)
        {
            throw new Exception("Vui lòng điền đủ thông tin hồ sơ");
        }
        if (student.Status == null)
        {
            throw new Exception("Vui lòng điền đủ thông tin hồ sơ");
        }if (student.ClassName == null)
        {
            throw new Exception("Vui lòng điền đủ thông tin hồ sơ");
        }
        try
        {
            _studentRepository.UpdateStudent(student);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void DeleteStudent(Student s)
    {
        _studentRepository.DeleteStudent(s);
    }

    public DataTable? SearchStudent(string keyword)
    {
        if (keyword == null)
        {
            throw new Exception("Vui lòng nhập từ khóa tìm kiếm");
        }
        try
        {
            return _studentRepository.SearchStudent(keyword);

        }
        catch (Exception e)
        {
            throw new Exception("Looix: " + e.Message);
        }
    }
}

