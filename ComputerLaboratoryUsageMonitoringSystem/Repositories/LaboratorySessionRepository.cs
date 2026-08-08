using ComputerLaboratoryUsageMonitoringSystem.Models;

namespace ComputerLaboratoryUsageMonitoringSystem.Repositories
{
    public class LaboratorySessionRepository : ILaboratorySessionRepository
    {
        private static readonly List<LaboratorySession> sessions = new();

        public List<LaboratorySession> GetAll()
        {
            return sessions;
        }

        public LaboratorySession? GetById(int id)
        {
            return sessions.FirstOrDefault(s => s.Id == id);
        }

        public void Add(LaboratorySession session)
        {
            session.Id = sessions.Count == 0
                ? 1
                : sessions.Max(s => s.Id) + 1;

            session.SessionNumber = sessions.Count == 0
                ? 1001
                : sessions.Max(s => s.SessionNumber) + 1;

            sessions.Add(session);
        }

        public void Update(LaboratorySession session)
        {
            var existingSession = GetById(session.Id);

            if (existingSession == null)
                return;

            existingSession.StudentNumber = session.StudentNumber;
            existingSession.FirstName = session.FirstName;
            existingSession.LastName = session.LastName;
            existingSession.Course = session.Course;
            existingSession.YearLevel = session.YearLevel;
            existingSession.ComputerNumber = session.ComputerNumber;
            existingSession.Purpose = session.Purpose;
            existingSession.TimeIn = session.TimeIn;
            existingSession.TimeOut = session.TimeOut;
            existingSession.Status = session.Status;
            existingSession.Notes = session.Notes;
        }

        public void Delete(int id)
        {
            var session = GetById(id);

            if (session != null)
            {
                sessions.Remove(session);
            }
        }
    }
}