public class User
{
    public long Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public int Role { get; set; } // 1: User, 0: Admin
}

