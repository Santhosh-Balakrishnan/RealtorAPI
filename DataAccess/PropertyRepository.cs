using DataAccess.Interfaces;
using Models;
using System.Reflection;

namespace DataAccess
{
    public class PropertyRepository:Repository<PropertyDetail>,IPropertyRepository
    {
        public PropertyRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            //entities=GetProperties();
        }

        private List<PropertyDetail> GetProperties()
        {
            return new List<PropertyDetail> {
                new PropertyDetail() { Id=1, Title="", Category=GetCategory(1),
                    BuiltYear=2000,Area=1200,Address="2604 Spring creek pkwy",City="Plano",State="Texas",
                Country="Us", NoOfBaths=2,NoOfBeds=2,Price=450000,ZipCode=75023,Description="Home Description"},
                new PropertyDetail() { Id=2, Title="", Category=GetCategory(2),
                    BuiltYear=2000,Area=1200,Address="2604 Spring creek pkwy",City="Princeton",State="New York",
                Country="Us", NoOfBaths=2,NoOfBeds=2,Price=450000,ZipCode=75023,Description="Home Description"},
                new PropertyDetail() { Id=3, Title="", Category=GetCategory(3),
                    BuiltYear=2000,Area=1200,Address="2604 Spring creek pkwy",City="Edison",State="New Jersy",
                Country="Us", NoOfBaths=2,NoOfBeds=2,Price=450000,ZipCode=75023,Description="Home Description"},
            };

        }
        string GetCategory(int id)
        {
            var category = (Category)id;
           return category.ToString();
        }

        public bool Update(PropertyDetail propertyDetail)
        {
            var property = Get(prop => prop.Id == propertyDetail.Id);
            if (property == null)
            {
                property.Title = propertyDetail.Title;
                return true;
            }
            return false;
        }
    }
}