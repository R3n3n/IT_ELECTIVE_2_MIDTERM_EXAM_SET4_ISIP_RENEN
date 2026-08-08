using ComputerLaboratoryUsageMonitoringSystem.Models;

namespace ComputerLaboratoryUsageMonitoringSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> users = new()
        {
            new User
            {
                Id = 1,
                FirstName = "Laboratory",
                LastName = "Assistant",
                Email = "internetlab1@lyceumofalabang.com",
                Username = "admin",
                Password = "admin123"
            }
        };

        public User? GetByUsername(string username)
        {
            return users.FirstOrDefault(
                u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
            );
        }

        public User? GetById(int id)
        {
            return users.FirstOrDefault(u => u.Id == id);
        }

        public void Add(User user)
        {
            user.Id = users.Count == 0
                ? 1
                : users.Max(u => u.Id) + 1;

            users.Add(user);
        }

        public bool UsernameExists(string username)
        {
            return users.Any(
                u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
            );
        }
    }
}