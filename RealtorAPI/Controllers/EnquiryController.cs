using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using RealtorAPI.Model;
using System.Net;

namespace RealtorAPI.Controllers
{
    public class EnquiryController : ControllerBase
    {
        IUnitOfWork unitOfWork;
        public EnquiryController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
      
        [HttpPost]
        public IActionResult GetEnquiries([FromBody]int agentId,[FromBody] SearchData searchData)
        {
            var properties = unitOfWork.EnquiryRepository.GetAll(enquiry =>
                    string.IsNullOrEmpty(searchData.State) ? true : enquiry.State == searchData.State &&
                    string.IsNullOrEmpty(searchData.City) ? true : enquiry.City == searchData.City &&
                    string.IsNullOrEmpty(searchData.Country) ? true : enquiry.Country == searchData.Country &&
                    enquiry.Id==searchData.AgentId 
                );
            return Ok(properties);
        }

        [HttpGet]
        public IActionResult GetEnquiry(int id)
        {
            var property= unitOfWork.EnquiryRepository.Get(enquiry => enquiry.Id == id);
            if(property == null)
            {
                return Problem("No Matching Property found !", statusCode: (int)HttpStatusCode.NotFound);
            }
            return Ok(property);
        }
       
        [HttpPost]
        public IActionResult AddEnquiry([FromBody]Enquiry enquiry)
        {
            if(!ModelState.IsValid)
            {
                return Problem("Invalid Enquiry",statusCode:(int)HttpStatusCode.NotAcceptable);
            }
            unitOfWork.EnquiryRepository.Add(enquiry);
            unitOfWork.SaveChanges();
            return Ok("Enquiry Created Successfully.");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var enquiry = unitOfWork.EnquiryRepository.Get(enq => enq.Id == id);
            if(enquiry == null)
            {
                return Problem("No Matching Property Exist",statusCode:(int)HttpStatusCode.NotFound) ;
            }
            unitOfWork.EnquiryRepository.Delete(enquiry);
            unitOfWork.SaveChanges();
            return Ok("Enquiry Removed Successfully");
        }
    }
}
