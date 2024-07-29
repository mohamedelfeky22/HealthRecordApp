using HealthRecordApp.DataService.Configuration;
using HealthRecordApp.Entities.DBSets;
using HealthRecordApp.Entities.Dtos.Incoming;
using Microsoft.AspNetCore.Mvc;

namespace HealthRecordApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var users= await _unitOfWork.Users.GetAll();
            return Ok(users);
        }

        //add /delete /
        [HttpPost("Add")]
        public async Task<IActionResult> Add(UserDto user)
        {
            var _user = new User()
            {
                firstName=user.firstName,
                lastName=user.lastName,
                email=user.email,
                phone=user.phone,
                country=user.country,
                dateOfBirth=Convert.ToDateTime(user.dateOfBirth),

            };
          var res=  await _unitOfWork.Users.add(_user);
          await  _unitOfWork.completeAsync();
            return CreatedAtRoute("GetUser", new { id= _user.Id }, _user);
        }


        [HttpGet]
        [Route("Get",Name ="GetUser")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _unitOfWork.Users.GetbyId(id);
            return Ok(user);
        }
    }
}
