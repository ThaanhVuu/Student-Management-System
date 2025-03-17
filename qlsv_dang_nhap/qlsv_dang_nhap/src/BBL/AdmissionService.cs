using System.Data;
class AdmissionService
{
    private readonly AdmissionRepository _admissionRepository;
    public AdmissionService()
    {
        _admissionRepository = new AdmissionRepository();
    }

    public DataTable? GetAllAdmissions() // lay ds
    {
        try
        {
            return _admissionRepository.GetAllAdmissions();

        }
        catch (Exception e)
        {
            throw new Exception("Lấy danh sách tuyển sinh lỗi: " + e.Message);
        }
    }

    public long RegisterAdmission(Admission admission)
    {
        if(admission.FullName == null)
        {
            throw new Exception("Tên không được để trống");
        }
        if(admission.DOB == null)
        {
            throw new Exception("Ngày sinh không được để trống");
        }
        if(admission.Gender == null)
        {
            throw new Exception("Giới tính không được để trống");
        }
        if(admission.ProgramId == null)
        {
            throw new Exception("Chương trình đào tạo không được để trống");
        }
        long id = _admissionRepository.AddAdmission(admission);
        return id;
    }

    public void UpdateAdmission(Admission admission) // sua
    {
        if (admission.AdmissionId == null)
        {
            throw new Exception("Vui lòng chọn sinh viên cần sửa thông tin");
        }
        if (admission.FullName == null)
        {
            throw new Exception("Tên không được để trống");
        }
        if (admission.DOB == null)
        {
            throw new Exception("Ngày sinh không được để trống");
        }
        if (admission.Gender == null)
        {
            throw new Exception("Giới tính không được để trống");
        }
        if (admission.ProgramId == null)
        {
            throw new Exception("Chương trình đào tạo không được để trống");
        }
        _admissionRepository.UpdateAdmission(admission);
    }

    public void DeleteAdmission(long admissionid)
    {
        if(admissionid == null)
        {
            throw new Exception("Vui lòng chọn sinh viên cần xóa");
        }
        _admissionRepository.DeleteAdmission(admissionid);
    }

    public DataTable? SearchAdmission(string keyword)
    {
        if (keyword == null)
        {
            throw new Exception("Vui lòng nhập từ khóa tìm kiếm");
        }
        try {
            return _admissionRepository.SearchAdmission(keyword);

        }
        catch(Exception e)
        {
            throw new Exception("Looix: " + e.Message);
        }
    }

    public void ApproveAdmission(Admission a)
    {
        if (a == null)
        {
            throw new ArgumentNullException("Đối tượng Admission không tồn tại.");
        }
        if (a.StatusAdmission == "Pending")
        {
            _admissionRepository.ApproveAdmission(a);
        }
        else if(a.StatusAdmission == "Approved")
        {
            throw new Exception("Trạng thái hồ sơ đã được duyệt");
        }
        else
        {
            throw new Exception("Trạng thái hồ sơ đã bị từ chối");
        }
    }

    public void RejectAdmission(Admission a)
    {
        if (a == null)
        {
            throw new ArgumentNullException( "Đối tượng Admission không tồn tại.");
        }
        if (a.StatusAdmission == "Pending")
        {
            _admissionRepository.RejectAdmission(a);
        }
        else if (a.StatusAdmission == "Approved")
        {
            throw new Exception("Trạng thái hồ sơ đã được duyệt");
        }
        else
        {
            throw new Exception("Trạng thái hồ sơ đã bị từ chối");
        }
    }
}