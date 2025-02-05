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
      
        public IActionResult Create([FromBody] JobApplication jobApplication)
        {
            if (jobApplication == null)
                return BadRequest("Invalid job application data");

            
            Console.WriteLine($"JobApplication data: {jobApplication.JobId}, {jobApplication.Name}, {jobApplication.Email}");

            if (jobApplication.JobId == 0)
                return BadRequest("JobId is required.");

         
            var job = _unitOfWork.Jobs.GetById(jobApplication.JobId);
            if (job == null)
                return BadRequest("Invalid JobId.");

            jobApplication.Job = job;

            _unitOfWork.JobApplications.Add(jobApplication);
            _unitOfWork.Complete(); 

            
            Console.WriteLine("Job application created successfully.");

            return CreatedAtAction(nameof(GetById), new { id = jobApplication.ApplicationId }, jobApplication);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] JobApplication jobApplication)
        {
            if (jobApplication == null || id != jobApplication.ApplicationId)
                return BadRequest("Invalid job application data");

            var existingJobApplication = _unitOfWork.JobApplications.GetById(id);
            if (existingJobApplication == null)
                return NotFound("Job application not found");

            _unitOfWork.JobApplications.Update(jobApplication);
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
    }
}
