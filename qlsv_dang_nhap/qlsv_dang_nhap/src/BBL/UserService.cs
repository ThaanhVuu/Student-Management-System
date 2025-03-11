using System.Data;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User Authenticate(string username, string password)
    {
        var user = _userRepository.GetUserByUsername(username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            throw new Exception("Thông tin đăng nhập không chính xác");

        return user;
    }

    public void RegisterUser(User newUser, string password)
    {
        if (_userRepository.GetUserByUsername(newUser.Username) != null)
            throw new Exception("Tên người dùng đã tồn tại");

        string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
        newUser.Password = BCrypt.Net.BCrypt.HashPassword(password, salt);

        _userRepository.AddUser(newUser);
    }

    public void DeleteUser(int userId)
    {
        _userRepository.DeleteUser(userId);
    }

    public void UpdateUserPassword(string username, string newPassword)
    {
        string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword,salt);
        _userRepository.UpdatePassword(username, hashedPassword);
    }

    public DataTable GetAllUsersDataTable()
    {
        return _userRepository.GetAllUsers();
    }
}