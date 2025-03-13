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

    }
    public void DeleteStudent(int id)
    {

    }

    public long getLastestAdmissionId()
    {
        return _studentRepository.getLastestAdmissionId();
    }
}

