using JobListAPI.Data.DataManagers;

namespace JobListAPI.Services;

[Service]
public class JobService
{
    private readonly IJobManager _jobManager;

    public JobService(IJobManager jobManager)
    {
        _jobManager = jobManager;
    }

}
