#region References
using Microsoft.AspNetCore.Mvc;
using BookingManagementCommon.DTO;
using BookingManagementContracts.Services;
#endregion

#region Namespace
namespace BookingManagementApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        /// <summary>
        /// The authentication service
        /// </summary>
        private readonly IAuthService _authService;
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authService">The authentication service.</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Authentications the login asynchronous.
        /// </summary>
        /// <param name="auth">The authentication.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> AuthLoginAsync([FromBody] AuthDto auth, CancellationToken cancellationToken)
        {
            try
            {
                var serviceResult = await _authService.AuthLoginAsync(auth);
                return StatusCode(statusCode: (serviceResult != null && serviceResult.isAuthenticated) ? StatusCodes.Status200OK : StatusCodes.Status401Unauthorized, serviceResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
#endregion