using System.Data;
class AdmissionSerVice
{
    private readonly AdmissionRepository _admissionRepository;
    public AdmissionSerVice(AdmissionRepository admissionRepository)
    {

        _admissionRepository = admissionRepository;
    }

    public DataTable? GetAllAdmissions() // lay ds
    {
        return _admissionRepository.GetAllAdmissions();
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
        if(admission.StatusAdmission == null)
        {
            throw new Exception("Trạng thái không được để trống");
        }
        if(admission.Gender == null)
        {
            throw new Exception("Giới tính không được để trống");
        }
        if(admission.ProgramId == null)
        {
            throw new Exception("Chương trình đào tạo không được để trống");
        }
        _admissionRepository.AddAdmission(admission);
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
        if (admission.StatusAdmission == null)
        {
            throw new Exception("Trạng thái không được để trống");
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
        return _admissionRepository.SearchAdmission(keyword);
    }
}