using HealthRecordApp.DataService.Configuration;
using HealthRecordApp.Entities.DBSets;
using HealthRecordApp.Entities.Dtos.Incoming;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthRecordApp.API.Controllers.V1
{

    public class UserController : BaseController
    {
        public UserController(IUnitOfWork unitOfWork):base(unitOfWork) { }
      

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _unitOfWork.Users.GetAll();
            return Ok(users);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(UserDto user)
        {
            var _user = new User()
            {
                firstName = user.firstName,
                lastName = user.lastName,
                email = user.email,
                phone = user.phone,
                country = user.country,
                dateOfBirth = Convert.ToDateTime(user.dateOfBirth),

            };
            var res = await _unitOfWork.Users.add(_user);
            await _unitOfWork.completeAsync();
            return CreatedAtRoute("GetUser", new { id = _user.Id }, _user);
        }


        [HttpGet]
        [Route("Get", Name = "GetUser")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _unitOfWork.Users.GetbyId(id);
            return Ok(user);
        }
    }
}
