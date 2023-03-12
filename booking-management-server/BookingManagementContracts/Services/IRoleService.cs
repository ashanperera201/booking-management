#region References
using BookingManagementCommon.DTO;
#endregion

#region Namespace
namespace BookingManagementContracts.Services
{
    public interface IRoleService
    {
        /// <summary>
        /// Saves the Role asynchronous.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<RoleDto> SaveRoleAsync(RoleDto role, CancellationToken cancellationToken = default);
        /// <summary>
        /// Updates the Role asynchronous.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<RoleDto> UpdateRoleAsync(RoleDto role, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets all Roles asynchronous.
        /// </summary>
        /// <param name="pagination">The pagination.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<PaginationResponseDto<RoleDto>> GetAllRolesAsync(PaginationDto pagination, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the Role asynchronous.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<RoleDto?> GetRoleAsync(string roleId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Deletes the Role asynchronous.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<bool> DeleteRoleAsync(string roleId, CancellationToken cancellationToken = default);
    }
}
#endregion