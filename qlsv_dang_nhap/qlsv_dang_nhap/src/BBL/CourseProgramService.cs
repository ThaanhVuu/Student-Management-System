
using System.Data;

class CourseProgramService
{
    private readonly CourseProgramRepository _repo;
    public CourseProgramService()
    {
        _repo = new CourseProgramRepository();
    }

    public DataTable getAllCourseProgram()
    {
        return _repo.getall();
    }
}

