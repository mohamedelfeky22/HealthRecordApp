using HealthRecordApp.DataService.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthRecordApp.API.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiversion}/[controller]")]
    [ApiVersion("1.0")]
    public class BaseController : ControllerBase
    {
        public IUnitOfWork _unitOfWork;
        public BaseController(IUnitOfWork unitOfWork)
        {
                _unitOfWork = unitOfWork;
        }
    }
}
