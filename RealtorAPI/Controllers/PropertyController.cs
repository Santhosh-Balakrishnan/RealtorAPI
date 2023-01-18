using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using RealtorAPI.Model;
using System.Net;

namespace RealtorAPI.Controllers
{
    public class PropertyController : ControllerBase
    {
        IUnitOfWork unitOfWork;
        public PropertyController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(unitOfWork.PropertyRepository.GetAll());
        }

        [HttpPost]
        public IActionResult GetProperties([FromBody] SearchData searchData)
        {
            var properties = unitOfWork.PropertyRepository.GetAll(property =>
                    string.IsNullOrEmpty(searchData.State) ? true : property.State == searchData.State &&
                    string.IsNullOrEmpty(searchData.City) ? true : property.City == searchData.City &&
                    string.IsNullOrEmpty(searchData.Country) ? true : property.Country == searchData.Country &&
                    string.IsNullOrEmpty(searchData.Category) ? true : property.Category == searchData.Category &&
                    searchData.AgentId ==null ? true:property.AgentId==searchData.AgentId && 
                    searchData.NoOfBaths == property.NoOfBaths &&
                    searchData.NoOfBeds == property.NoOfBeds &&
                    property.Price >= searchData.Price.Min && property.Price <= searchData.Price.Max &&
                    property.Area >= searchData.Area.Min && property.Area <= searchData.Area.Max
                );
            return Ok(properties);
        }

        [HttpGet]
        public IActionResult GetProperty(int id)
        {
            var property= unitOfWork.PropertyRepository.Get(prop => prop.Id == id);
            if(property == null)
            {
                return Problem("No Matching Property found !", statusCode: (int)HttpStatusCode.NotFound);
            }
            return Ok(property);
        }
       
        [HttpPost]
        public IActionResult AddProperty([FromBody]PropertyDetail property)
        {
            if(!ModelState.IsValid)
            {
                return Problem("Invalid Property Information",statusCode:(int)HttpStatusCode.NotAcceptable);
            }
            unitOfWork.PropertyRepository.Add(property);
            unitOfWork.SaveChanges();
            return Ok("Property Added Successfully.");
        }

        [HttpPut]
        public IActionResult UpdateProperty([FromBody] PropertyDetail propertyDetail)
        {
            if (!ModelState.IsValid)
            {
                return Problem("Invalid Property Information", statusCode: (int)HttpStatusCode.NotAcceptable);
            }
            var property = unitOfWork.PropertyRepository.Get(prop => prop.Id == propertyDetail.Id);
            if (property==null)
            {
                return Problem("Property Not Available To Update", statusCode: (int)HttpStatusCode.NotFound);
            }
            unitOfWork.PropertyRepository.Update(propertyDetail);
            unitOfWork.SaveChanges();
            return Ok("Property Updated Successfully");

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var property = unitOfWork.PropertyRepository.Get(prop => prop.Id == id);
            if(property == null)
            {
                return Problem("No Matching Property Exist",statusCode:(int)HttpStatusCode.NotFound);
            }
            unitOfWork.PropertyRepository.Delete(property);
            unitOfWork.SaveChanges();
            return Ok("Property Removed Successfully");
        }
    }
}
