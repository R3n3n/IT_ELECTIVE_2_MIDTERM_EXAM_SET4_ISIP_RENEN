using ComputerLaboratoryUsageMonitoringSystem.Models;
namespace ComputerLaboratoryUsageMonitoringSystem.Repositories
{
    public interface IUserRepository
    {
        User? GetByUsername(string username);
        User? GetById(int Id);
        void Add(User user);
        bool UsernameExists(string username);

    }
}
