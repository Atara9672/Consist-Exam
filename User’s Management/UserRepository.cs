using Newtonsoft.Json;
using System.Text;


namespace User_s_Management
{
    public class UserRepository : IUserRepository
    {
        private readonly string _filePath = "users.json";

        public UserRepository()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, JsonConvert.SerializeObject(new List<User>()));
            }
        }

        public List<User> GetAllUsers()
        {
            var jsonData = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<User>>(jsonData) ?? new List<User>();
        }

        public string EncodePassword(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }

        public string DecodePassword(string encodedPassword)
        {
            byte[] passwordBytes = Convert.FromBase64String(encodedPassword);
            return Encoding.UTF8.GetString(passwordBytes);
        }

        public User CreateUser(User user)
        {
            List<User> users = GetAllUsers();
            if (users.Exists(u => u.UserName == user.UserName))
                return null;
            user.UserId = Guid.NewGuid();
            user.UserPassword = EncodePassword(user.UserPassword);
            users.Add(user);
            File.WriteAllText(_filePath, JsonConvert.SerializeObject(users));
            return user;
        }

        public bool DeleteUser(Guid userId)
        {
            List<User> users = GetAllUsers();
            var user = users.FirstOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                users.Remove(user);
                File.WriteAllText(_filePath, JsonConvert.SerializeObject(users));
                return true;
            }
            return false;
        }

        public User ValidateUser(string username, string password)
        {
            List<User> users = GetAllUsers();
            return users.FirstOrDefault(u => u.UserName == username && DecodePassword(u.UserPassword) == password);
        }
    }

}
