using DataAccess.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserRepository : Repository<UserInfo>, IUserRepository
    {
        public UserRepository(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {

        }
    }
}
