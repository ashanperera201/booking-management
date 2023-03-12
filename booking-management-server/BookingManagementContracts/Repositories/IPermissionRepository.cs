#region References
using BookingManagementDomain.EntityModels;
#endregion

#region Namespace
namespace BookingManagementContracts.Repositories
{
    public interface IPermissionRepository
    {
        /// <summary>
        /// Saves the permission asynchronous.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Permission> SavePermissionAsync(Permission permission, CancellationToken cancellationToken = default);
        /// <summary>
        /// Updates the permissin asynchronous.
        /// </summary>
        /// <param name="permissionId">The permission identifier.</param>
        /// <param name="permission">The permission.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Permission> UpdatePermissinAsync(Permission permission, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets all permissions asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        IQueryable<Permission> GetAllPermissionsAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the permission asynchronous.
        /// </summary>
        /// <param name="permissionId">The permission identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Permission?> GetPermissionAsync(string permissionId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Deletes the permission.
        /// </summary>
        /// <param name="permissionId">The permission identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<bool> DeletePermissionAsync(string permissionId, CancellationToken cancellationToken = default);
    }
}
#endregion