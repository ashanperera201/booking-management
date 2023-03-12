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
    [Route("api/v{version:apiVersion}/permission")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PermissionController : BaseController
    {
        /// <summary>
        /// The permission service
        /// </summary>
        private readonly IPermissionService _permissionService;
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionController"/> class.
        /// </summary>
        /// <param name="permissionService">The permission service.</param>
        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        /// <summary>
        /// Saves the permission asynchronous.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SavePermissionAsync([FromBody] PermissionDto permission, CancellationToken cancellationToken)
        {
            try
            {
                var savedResult = await _permissionService.SavePermissionAsync(permission, cancellationToken);
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
        /// <param name="permission">The permission.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdatePermissionAsync([FromBody] PermissionDto permission, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _permissionService.UpdatePermissionAsync(permission, cancellationToken);
                return StatusCode(statusCode: result != null ? StatusCodes.Status200OK : StatusCodes.Status500InternalServerError, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets all permissions asynchronous.
        /// </summary>
        /// <param name="pagination">The pagination.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet("query")]
        public async Task<IActionResult> GetAllPermissionsAsync([FromQuery] PaginationDto pagination, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _permissionService.GetAllPermissionsAsync(pagination, cancellationToken);
                return StatusCode(statusCode: result != null ? StatusCodes.Status200OK : StatusCodes.Status204NoContent, result);
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
        [HttpGet("{permissionId}")]
        public async Task<IActionResult> GetPermissionAsync(string permissionId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _permissionService.GetPermissionAsync(permissionId, cancellationToken);
                return StatusCode(statusCode: result != null ? StatusCodes.Status200OK : StatusCodes.Status204NoContent, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Deletes the permission asynchronous.
        /// </summary>
        /// <param name="permissionId">The permission identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpDelete("{permissionId}")]
        public async Task<IActionResult> DeletePermissionAsync(string permissionId, CancellationToken cancellationToken)
        {
            try
            {
                var servicesRes = await _permissionService.DeletePermissionAsync(permissionId, cancellationToken);
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