using JobPortal.core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.EF
{
    public class JobPortalDbContext: DbContext
    {
        public JobPortalDbContext(DbContextOptions<JobPortalDbContext> options) : base(options)
        {
            
        }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Default values for columns
            modelBuilder.Entity<Job>()
                .Property(j => j.PostedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<JobApplication>()
                .Property(ja => ja.AppliedDate)
                .HasDefaultValueSql("GETUTCDATE()");


            modelBuilder.Entity<Job>().HasData(
                new Job
                {
                    JobId = 1,
                    Title = "Software Engineer",
                    Company = "TechCorp",
                    Location = "New York",
                    Description = "Develop and maintain web applications.",
                    Requirements = "3+ years experience in C# and ASP.NET.",
                    PostedDate = new DateTime(2025, 1, 1)
                },
                new Job
                {
                    JobId = 2,
                    Title = "Data Analyst",
                    Company = "DataCorp",
                    Location = "San Francisco",
                    Description = "Analyze large datasets and provide insights.",
                    Requirements = "Experience with SQL and Power BI.",
                    PostedDate = new DateTime(2025, 1, 2)
                }
            );

            modelBuilder.Entity<JobApplication>().HasData(
                new JobApplication
                {
                    ApplicationId = 1,
                    JobId = 1,
                    Name = "John Doe",
                    Email = "johndoe@example.com",
                    ResumeUrl = "resumes/johndoe.pdf",
                    AppliedDate = new DateTime(2025, 1, 3)
                },
                new JobApplication
                {
                    ApplicationId = 2,
                    JobId = 2,
                    Name = "Jane Smith",
                    Email = "janesmith@example.com",
                    ResumeUrl = "resumes/janesmith.pdf",
                    AppliedDate = new DateTime(2025, 1, 4)
                }
            );
        }
    }
}
