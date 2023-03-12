#region References
using Microsoft.AspNetCore.Mvc;
using BookingManagementCommon.DTO;
using BookingManagementContracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
#endregion

#region Namespace
namespace BookingManagementApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/user")]
    [ApiController]
    public class UserController : BaseController
    {
        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService _userService;
        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Saves the role asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveUserAsync([FromBody] UserDto user, CancellationToken cancellationToken)
        {
            try
            {
                var savedResult = await _userService.SaveUserAsync(user, cancellationToken);
                return StatusCode(statusCode: savedResult != null ? StatusCodes.Status200OK : StatusCodes.Status500InternalServerError, savedResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Updates the permission asynchronous.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UserDto user, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.UpdateUserAsync(user, cancellationToken);
                return StatusCode(statusCode: result != null ? StatusCodes.Status200OK : StatusCodes.Status500InternalServerError, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the permission asynchronous.
        /// </summary>
        /// <param name="permissionId">The permission identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUserAsync(string userId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.GetUserAsync(userId, cancellationToken);
                return StatusCode(statusCode: result != null ? StatusCodes.Status200OK : StatusCodes.Status204NoContent, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAllUserDetails()
        {
            try
            {
                var result = await _userService.GetAllUsersAsync();
                return StatusCode(statusCode: result != null ? StatusCodes.Status200OK : StatusCodes.Status204NoContent, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
#endregion