using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
       IUserRepository UserRepository { get; set; }
       IPropertyRepository PropertyRepository { get; set; }
       IEnquiryRepository EnquiryRepository { get; set; }

       void SaveChanges();
    }
}
