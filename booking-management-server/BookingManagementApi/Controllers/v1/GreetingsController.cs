#region References
using Microsoft.AspNetCore.Mvc;
#endregion

#region Namespace
namespace BookingManagementApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/greetings")]
    [ApiController]
    public class GreetingsController : BaseController
    {
        [HttpGet]
        public IActionResult GetGreetings()
        {
            return Ok("Welcome to booking service.");
        }
    }
}
#endregion