using JobListAPI.Models;

namespace JobListAPI.DTOs;

public class JobRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public JobStatus Status { get; set; }
    public string Notes { get; set; } = string.Empty;
}
