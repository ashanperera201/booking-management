#region References
using BookingManagementCommon.DTO;
#endregion

#region Namespace

namespace BookingManagementContracts.Services
{
    public interface IPermissionService
    {
        /// <summary>
        /// Saves the permission asynchronous.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<PermissionDto> SavePermissionAsync(PermissionDto permission, CancellationToken cancellationToken = default);
        /// <summary>
        /// Updates the permission asynchronous.
        /// </summary>
        /// <param name="pagination">The pagination.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<PermissionDto> UpdatePermissionAsync(PermissionDto permission, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets all permissions asynchronous.
        /// </summary>
        /// <param name="pagination">The pagination.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<PaginationResponseDto<PermissionDto>> GetAllPermissionsAsync(PaginationDto pagination, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the permission asynchronous.
        /// </summary>
        /// <param name="permissionId">The permission identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<PermissionDto?> GetPermissionAsync(string permissionId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Deletes the permission asynchronous.
        /// </summary>
        /// <param name="permissionId">The permission identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<bool> DeletePermissionAsync(string permissionId, CancellationToken cancellationToken = default);
    }
}
#endregion