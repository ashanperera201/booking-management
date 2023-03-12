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
    [Route("api/v{version:apiVersion}/role")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RoleController : BaseController
    {
        /// <summary>
        /// The role service
        /// </summary>
        private readonly IRoleService _roleService;
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleController"/> class.
        /// </summary>
        /// <param name="roleService">The role service.</param>
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Saves the role asynchronous.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveRoleAsync([FromBody] RoleDto role, CancellationToken cancellationToken)
        {
            try
            {
                var savedResult = await _roleService.SaveRoleAsync(role, cancellationToken);
                return StatusCode(statusCode: savedResult != null ? StatusCodes.Status200OK : StatusCodes.Status500InternalServerError, savedResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Updates the role asynchronous.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateRoleAsync([FromBody] RoleDto role, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _roleService.UpdateRoleAsync(role, cancellationToken);
                return StatusCode(statusCode: result != null ? StatusCodes.Status200OK : StatusCodes.Status500InternalServerError, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets all roles asynchronous.
        /// </summary>
        /// <param name="pagination">The pagination.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet("query")]
        public async Task<IActionResult> GetAllRolesAsync([FromQuery] PaginationDto pagination, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _roleService.GetAllRolesAsync(pagination, cancellationToken);
                return StatusCode(statusCode: result != null ? StatusCodes.Status200OK : StatusCodes.Status204NoContent, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the role asynchronous.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetRoleAsync(string roleId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _roleService.GetRoleAsync(roleId, cancellationToken);
                return StatusCode(statusCode: result != null ? StatusCodes.Status200OK : StatusCodes.Status204NoContent, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Deletes the role asynchronous.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteRoleAsync(string roleId, CancellationToken cancellationToken)
        {
            try
            {
                var servicesRes = await _roleService.DeleteRoleAsync(roleId, cancellationToken);
                return StatusCode(servicesRes ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, servicesRes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
#endregion