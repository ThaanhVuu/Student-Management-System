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
}