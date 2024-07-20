using JobListAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobListAPI.Data.DataManagers
{
    public class JobManager : IJobManager
    {
        private ApplicationDbContext _context;

        public JobManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<int> CreateJob(Job job)
        {
            _context.Jobs.Add(job);

            return _context.SaveChangesAsync();
        }

        public Task<int> DeleteJob(int id)
        {
            Job job = _context.Jobs.First(x => x.Id == id);
            _context.Jobs.Remove(job);

            return _context.SaveChangesAsync();
        }

        public Task<int> UpdateJob(Job job)
        {
            _context.Jobs.Update(job);

            return _context.SaveChangesAsync();
        }

        public Task<Job> GetJobById(int id)
        {
            return _context.Jobs.FirstAsync(x => x.Id == id);
        }

        public Task<List<Job>> GetAll()
        {
            return _context.Jobs
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }
    }
}
