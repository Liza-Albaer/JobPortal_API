using JobPortal.core.IUnitOfWork;
using JobPortal.core.Models;
using JobPortal.core.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public JobController(IUnitOfWork unitOfWork)
        {
            
            _unitOfWork = unitOfWork;
        }
     
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var job = _unitOfWork.Jobs.GetById(id);
            if (job == null)
                return NotFound("Job not found");

            return Ok(job);
        }

      
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var jobs = _unitOfWork.Jobs.GetAll();
            return Ok(jobs);
        }

       
        [HttpPost]
        public IActionResult Create([FromBody] Job job)
        {
            if (job == null)
                return BadRequest("Invalid job data");

            _unitOfWork.Jobs.Add(job);
            return CreatedAtAction(nameof(GetById), new { id = job.JobId }, job);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Job job)
        {
            if (job == null || id != job.JobId)
                return BadRequest("Invalid job data");

            var existingJob = _unitOfWork.Jobs.GetById(id);
            if (existingJob == null)
                return NotFound("Job not found");

            _unitOfWork.Jobs.Update(job);
            return Ok("Job updated successfully");
        }

      
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var job = _unitOfWork.Jobs.GetById(id);
            if (job == null)
                return NotFound("Job not found");

            _unitOfWork.Jobs.Delete(id);
            return Ok("Job deleted successfully");
        }
    }
}
