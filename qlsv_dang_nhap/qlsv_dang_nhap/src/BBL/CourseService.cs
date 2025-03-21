using System.Data;

class CourseService
{
    private readonly CourseRepository _courseRepository;

    public CourseService()
    {
        _courseRepository = new CourseRepository();
    }

    public DataTable getCourse()
    {
        return _courseRepository.getCourse();
    }

    public void saverCourse(DataTable dt)
    {
        foreach (DataRow row in dt.Rows)
        {
            if (row.RowState != DataRowState.Deleted)
            {
                int credit = Convert.ToInt32(row["Credit"]);
                if (credit < 0)
                {
                    throw new Exception("Môn học phải được đặt tín chỉ");
                }
                string name = Convert.ToString(row["course_name"]);
                if (name == null || credit == null)
                {
                    throw new Exception("Vui lòng điền đầy đủ thông tin");
                }
            }
        }
        _courseRepository.UpdateCourse(dt);
    }

    public void AddCourse(DataTable dt, string name, int credit)
    {
        if (name == null || credit == null)
        {
            throw new Exception("Vui lòng điền đủ thông tin");
        }
        else
        {
            DataRow newRow = dt.NewRow();
            newRow["course_name"] = name;
            newRow["credit"] = credit;
            dt.Rows.Add(newRow);
        }
        
    }

    public void DeleteCourse(DataTable dt, int id)
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



    public void EditCourse(DataTable dt, int id, string name, int credit)
    {
        foreach (DataRow row in dt.Rows)
        {
            if (Convert.ToInt32(row["course_id"]) == id)
            {
                row["course_name"] = name;
                row["credit"] = credit;
                return;
            }
        }
        throw new Exception("Khong tim thay mon hoc can sua" + id);
    }
}
