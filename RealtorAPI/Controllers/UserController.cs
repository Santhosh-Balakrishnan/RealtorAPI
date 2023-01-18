using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using RealtorAPI.Utilities;
using System.Net;
using System.Text;

namespace RealtorAPI.Controllers
{
    public class UserController : ControllerBase
    {
        IUnitOfWork unitOfWork;
        string apiKey;
        public UserController(IUnitOfWork unitOfWork,IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            apiKey=configuration.GetSection("ApiKey").ToString();
        }

        [HttpGet]
        public IActionResult Validate()
        {
            string authHeader = HttpContext.Request.Headers["Authorization"];
            if(authHeader != null)
            {
                var data = authHeader.Split(' ')[1];
                var credential=Encoding.UTF8.GetString(Convert.FromBase64String(data));
                var email = credential.Split(':')[0];
                var password = credential.Split(':')[1];
                var user = unitOfWork.UserRepository.Get(u => u.Email == email && u.Password == password);
                if (user != null)
                {
                    var token = JWTTokenGenerator.GenerateJWTToken(user, apiKey);
                    return Ok(token);
                }
                else return Problem(detail: "User not Exist", statusCode: (int)HttpStatusCode.NotFound);
            }

            return Problem(detail: "User not Exist", statusCode: (int)HttpStatusCode.NotFound);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody]UserInfo userInfo)
        {
            var user = unitOfWork.UserRepository.Get(u => u.Email == userInfo.Email);
            if (user == null)
            {
                unitOfWork.UserRepository.Add(userInfo);
                unitOfWork.SaveChanges();
                return Ok("User Added Successfully");
            }
            return Problem("User already exist with this email", statusCode: (int)HttpStatusCode.NotFound);
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserInfo userInfo)
        {
            var user = unitOfWork.UserRepository.Get(u => u.Email == userInfo.Email);
            if (user != null)
            {
                user.Email = userInfo.Email;
                unitOfWork.SaveChanges();
                return Ok("User Updated Successfully");
            }
            return Problem("User not exist", statusCode: (int)HttpStatusCode.NotFound);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var user = unitOfWork.UserRepository.Get(u => u.Id == id);
            if (user != null)
            {
                unitOfWork.UserRepository.Delete(user);
                unitOfWork.SaveChanges();
                return Ok("User Deleted Successfully");
            }
            return Problem("User not exist", statusCode: (int)HttpStatusCode.NotFound);
        }
    }
}
