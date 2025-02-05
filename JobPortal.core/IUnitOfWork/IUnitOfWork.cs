using JobPortal.core.Models;
using JobPortal.core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.core.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Job> Jobs { get; }
        IBaseRepository<JobApplication> JobApplications { get; }
        int Complete();
    }
}
