using System.Data;

class CourseProgramService
{
    private readonly CourseProgramRepository _repo;
    public CourseProgramService()
    {
        _repo = new CourseProgramRepository();
    }

    public DataTable getAllCourseProgram(CourseProgram cs)
    {
        return _repo.getall(cs);
    }

    public void addCP(int id, int idp)
    {
        if (id == null)
        {
            throw new ArgumentNullException("Vui lòng chọn môn học cần thêm vào chương trình đào tạo");
        } if (idp == null)
        {
            throw new ArgumentNullException("Chưa chọn chương trình đào tạo");
        }
        _repo.addCP(id, idp);
    }

    public void removeCP(int id)
    {
        _repo.deleteCP(id);
    }
}

