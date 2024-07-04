namespace User_s_Management
{
    public interface IUserRepository
    {
        User CreateUser(User user);
        bool DeleteUser(Guid userId);
        User ValidateUser(string username, string password);
    }

}
