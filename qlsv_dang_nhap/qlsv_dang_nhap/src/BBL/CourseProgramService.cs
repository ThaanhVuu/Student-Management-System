using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

