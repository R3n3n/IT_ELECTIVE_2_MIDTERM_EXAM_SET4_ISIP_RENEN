using ComputerLaboratoryUsageMonitoringSystem.Models;

namespace ComputerLaboratoryUsageMonitoringSystem.Repositories
{
    public interface ILaboratorySessionRepository
    {
        List<LaboratorySession> GetAll();
        LaboratorySession? GetById(int id);
        void Add(LaboratorySession session);
        void Update(LaboratorySession session);
        void Delete(int id);
    }
}