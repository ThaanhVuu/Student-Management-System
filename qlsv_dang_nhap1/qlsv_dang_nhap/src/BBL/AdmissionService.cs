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

    public void RegisterAdmission(Admission admission) // them
    {
        _admissionRepository.AddAdmission(admission);
    }

    public void UpdateAdmission(Admission admission) // sua
    {
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