using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        ApplicationDbContext context;

        public DbInitializer(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Initialize()
        {
            if(context.Database.GetPendingMigrations().Count()>0)
            {
                context.Database.Migrate();
            }
        }
    }
}
