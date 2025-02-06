using JobPortal.core.IUnitOfWork;
using JobPortal.core.Models;
using JobPortal.core.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobApplicationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public JobApplicationController(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }
       
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var jobApplication = _unitOfWork.JobApplications.GetById(id);
            if (jobApplication == null)
                return NotFound("Job application not found");

            return Ok(jobApplication);
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var jobApplications = _unitOfWork.JobApplications.GetAll();
            return Ok(jobApplications);
        }
        [HttpPost]
      
        public IActionResult Create([FromBody] JobApplicationDto jobApplicationDto)
        {
            if (jobApplicationDto == null)
                return BadRequest("Invalid job application data");

            
            Console.WriteLine($"JobApplication data: {jobApplicationDto.JobId}, {jobApplicationDto.Name}, {jobApplicationDto.Email}");

            if (jobApplicationDto.JobId == 0)
                return BadRequest("JobId is required.");

         
            var job = _unitOfWork.Jobs.GetById(jobApplicationDto.JobId);
            if (job == null)
                return BadRequest("Invalid JobId.");

            var jobapp=new JobApplication();
            jobapp.JobId = jobApplicationDto.JobId;
            jobapp.AppliedDate=DateTime.Now;
            jobapp.Email = jobApplicationDto.Email;
            jobapp.Name = jobApplicationDto.Name;
            jobapp.Job = job;
            jobapp.ResumeUrl= jobApplicationDto.ResumeUrl;

            _unitOfWork.JobApplications.Add(jobapp);
            _unitOfWork.Complete(); 

            
            Console.WriteLine("Job application created successfully.");

            return CreatedAtAction(nameof(GetById), new { id = jobapp.ApplicationId }, jobapp);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] JobApplicationDto jobApplicationDto)
        {
            if (jobApplicationDto == null || id == null)
                return BadRequest("Invalid job application data");

            var existingJobApplication = _unitOfWork.JobApplications.GetById(id);
            if (existingJobApplication == null)
                return NotFound("Job application not found");
            existingJobApplication.Name = jobApplicationDto.Name;
            existingJobApplication.JobId = jobApplicationDto.JobId;
            var jobinfo=_unitOfWork.Jobs.GetById(jobApplicationDto.JobId);
            existingJobApplication.Job = jobinfo;
            existingJobApplication.ResumeUrl = jobApplicationDto.ResumeUrl;
            existingJobApplication.Email = jobApplicationDto.Email;
            _unitOfWork.JobApplications.Update(existingJobApplication);
            return Ok("Job application updated successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var jobApplication = _unitOfWork.JobApplications.GetById(id);
            if (jobApplication == null)
                return NotFound("Job application not found");

            _unitOfWork.JobApplications.Delete(id);
            return Ok("Job application deleted successfully");
        }
        public class JobApplicationDto
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string ResumeUrl { get; set; }
            public int JobId { get; set; }
        }
    }
}
