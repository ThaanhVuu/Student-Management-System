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
        try
        {
            return _programReposity.GetAllPrograms();

        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public void RegisterProgram(Program program)
    {
        if (program.ProgramID == null || string.IsNullOrEmpty(program.ProgramName))
        {
            throw new Exception("Vui lòng nhập thông tin đầy đủ");
        }
        _programReposity.AddProgram(program);
    }

    public void UpdateProgram(Program program)
    {
        if (string.IsNullOrEmpty(program.ProgramName))
        {
            throw new Exception("Vui lòng nhập tên chương trình đào tạo");
        }
        if (program.ProgramID == null)
        {
            throw new Exception("Vui lòng chọn chương trình đào tạo");
        }
        try
        {
            _programReposity.UpdateProgramName(program);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public void DeleteProgram(int programId)
    {
        var program = _programReposity.GetProgramById(null, programId);
        if (program.ProgramID == null)
        {
            MessageBox.Show("Chương trình đào tạo không tồn tại");
        }
        _programReposity.DeleteProgram(programId);
    }
}