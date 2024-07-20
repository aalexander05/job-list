using JobListAPI.Models;

namespace JobListAPI.Data.DataManagers
{
    public interface IJobManager
    {
        Task<int> CreateJob(Job Job);
        Task<int> DeleteJob(int id);
        Task<int> UpdateJob(Job Job);

        Task<Job> GetJobById(int id);
        Task<List<Job>> GetAll();
    }
}
