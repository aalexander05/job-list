using AutoMapper;
using JobListAPI.Data.DataManagers;
using JobListAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobListAPI.Controllers;

[Authorize]
[Route("[controller]")]
public class JobController : ControllerBase
{
    private readonly IJobManager _jobManager;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JobController(IJobManager jobManager, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _jobManager = jobManager;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpPost]
    public async Task<ActionResult<DTOs.Job>> CreateJob([FromBody] DTOs.JobRequest job)
    {
        Job newJob = _mapper.Map<Job>(job);
        
        await _jobManager.CreateJob(newJob);

        DTOs.Job jobDto = _mapper.Map<DTOs.Job>(newJob);

        return CreatedAtAction("GetJobById", new { id = jobDto.Id }, jobDto);

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteJob(int id)
    {
        await _jobManager.DeleteJob(id);
        return NoContent();
    }


    [HttpPut]
    public async Task<IActionResult> UpdateJob([FromBody] DTOs.JobRequest job)
    {
        Job jobToUpdate = await _jobManager.GetJobById(job.Id);

        if (jobToUpdate == null)
        {
            return NotFound();
        }

        // var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;

        // var claim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("name", StringComparison.CurrentCultureIgnoreCase));

        _mapper.Map(job, jobToUpdate);

        await _jobManager.UpdateJob(jobToUpdate);
        return NoContent();
    }

    [HttpGet("{id}", Name = "GetJobById")]
    public async Task<ActionResult<DTOs.JobRequest>> GetJobById(int id)
    {
        Job job = await _jobManager.GetJobById(id);

        DTOs.JobRequest jobDto = _mapper.Map<DTOs.JobRequest>(job);

        

        return Ok(jobDto);
    }

    [HttpGet("View/{id}")]
    public async Task<ActionResult<DTOs.Job>> GetJobByIdForView(int id)
    {
        Job job = await _jobManager.GetJobById(id);

        DTOs.Job jobDto = _mapper.Map<DTOs.Job>(job);

        return Ok(jobDto);
    }

    [HttpGet]
    public async Task<ActionResult<DTOs.Job>> GetAllJobs()
    {
        IEnumerable<Job> jobs = await _jobManager.GetAll();

        IEnumerable<DTOs.Job> jobDtos = jobs.Select(x => _mapper.Map<DTOs.Job>(x));

        return Ok(jobDtos);
    }
}
