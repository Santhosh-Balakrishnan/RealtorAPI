using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IPropertyRepository:IRepository<PropertyDetail>
    {
       bool Update(PropertyDetail propertyDetail);
    }
}
