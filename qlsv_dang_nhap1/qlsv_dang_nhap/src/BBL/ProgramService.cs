using System.Data;
using System.Windows;
public class ProgramService
{
    private readonly ProgramRepository _programReposity;
    public ProgramService(ProgramRepository programReposity)
    {
        _programReposity = programReposity;
    }

    public DataTable GetAllPrograms()
    {
        return _programReposity.GetAllPrograms();
    }

    public void RegisterProgram(int programId,string programName, Program program)
    {
        if(string.IsNullOrEmpty(program.ProgramName) && string.IsNullOrEmpty(program.ProgramName))
        {
            MessageBox.Show("Vui lòng nhập thông tin đầy đủ");
        }else if(_programReposity.GetProgramById(program.ProgramName, program.ProgramID) != null)
        {
            MessageBox.Show("Chương trình đào tạo đã tồn tại");
        }
        _programReposity.AddProgram(new Program
            {
                ProgramID = programId,
                ProgramName = programName
            });
    }

    public void UpdateProgram(int programId, string newProgramName)
    {
        if (string.IsNullOrEmpty(newProgramName))
        {
            MessageBox.Show("Vui lòng nhập thông tin đầy đủ");
        }
        _programReposity.UpdateProgramName(programId, newProgramName);
    }

    public void DeleteProgram(int programId)
    {
        var program = _programReposity.GetProgramById(null,programId);
        if (program.ProgramID == null)
        {
            MessageBox.Show("Chương trình đào tạo không tồn tại");
        }
        _programReposity.DeleteProgram(programId);
    }
}