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

    public void saveCourseProgram(DataTable dt)
    {
        _repo.addCourseToCP(dt);
    }

    public void DeleteCourseFromCP(DataTable dt, int id)
    {
        foreach (DataRow row in dt.Rows)
        {
            if (Convert.ToInt32(row["course_id"]) == id)
            {
                row.Delete();
                return;
            }
        }
        throw new Exception("Khong tim thay mon hoc can xoa" + id);
    }
}

