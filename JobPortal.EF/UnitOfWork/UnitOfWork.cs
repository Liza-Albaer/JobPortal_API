using JobPortal.core.IUnitOfWork;
using JobPortal.core.Models;
using JobPortal.core.Repository;
using JobPortal.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.EF.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly JobPortalDbContext _context;
        public IBaseRepository<Job> Jobs { get; private set; }
        public IBaseRepository<JobApplication> JobApplications { get;private set; }
        public UnitOfWork(JobPortalDbContext context)
        {
            _context = context;
            Jobs =new BaseRepository<Job>(_context);
            JobApplications = new BaseRepository<JobApplication>(_context);
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
