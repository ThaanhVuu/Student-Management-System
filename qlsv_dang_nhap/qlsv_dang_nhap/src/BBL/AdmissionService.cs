using System.Data;
class AdmissionService
{
    private readonly AdmissionRepository _admissionRepository;
    public AdmissionService(AdmissionRepository admissionRepository)
    {
        _admissionRepository = admissionRepository;
    }

    public DataTable? GetAllAdmissions() // lay ds
    {
        try
        {
            return _admissionRepository.GetAllAdmissions();

        }
        catch (Exception e)
        {
            throw new Exception("Lấy ds: " + e.Message);
        }
    }

    public void RegisterAdmission(Admission admission)
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
        _admissionRepository.AddAdmissionAsync(admission);
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

    public void ApproveAdmission(long admissionId)
    {
        _admissionRepository.ApproveAdmission(admissionId);
    }

    public void RejectAdmission(long admissionId)
    {
        _admissionRepository.RejectAdmission(admissionId);
    }
}