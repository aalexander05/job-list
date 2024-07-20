using System.ComponentModel;

namespace JobListAPI.Models;

public class Job : IAuditable
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public JobStatus Status { get; set; }
    public string Notes { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set;}
    public DateTime UpdatedDate { get; set;}


}

public enum JobStatus
{
    [Description("Application Sent")]
    ApplicationSent,

    [Description("Rejected")]
    Rejected,

    [Description("Interview Scheduled")]
    InterviewScheduled,

    [Description("Offer Received")]
    OfferReceived,

    [Description("Hired")]
    Hired
}
