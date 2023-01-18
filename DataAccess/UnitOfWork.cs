using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository UserRepository { get ; set ; }
        public IPropertyRepository PropertyRepository { get ; set ; }
        public IEnquiryRepository EnquiryRepository { get ; set ; }
        ApplicationDbContext _dbContext ;
        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
            UserRepository =new UserRepository(applicationDbContext);
            EnquiryRepository=new EnquiryRepository(applicationDbContext);
            PropertyRepository = new PropertyRepository(applicationDbContext);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
