#region References
using BookingManagementDomain.EntityModels;
#endregion

#region Namespace

namespace BookingManagementContracts.Repositories
{
    public interface IRoleRepository
    {
        /// <summary>
        /// Saves the role asynchronous.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Role> SaveRoleAsync(Role role, CancellationToken cancellationToken = default);
        /// <summary>
        /// Updates the role asynchronous.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Role> UpdateRoleAsync(Role role, CancellationToken cancellationToken = default);
        /// <summary>
        /// Deletes the role asynchronous.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<bool> DeleteRoleAsync(string roleId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the role asynchronous.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Role> GetRoleAsync(string roleId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the role details asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        IQueryable<Role> GetRoleDetailsAsync(CancellationToken cancellationToken = default);
    }
}
#endregion