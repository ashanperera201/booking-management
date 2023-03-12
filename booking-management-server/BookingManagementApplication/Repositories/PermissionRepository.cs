#region References
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using BookingManagementContracts.Repositories;
using BookingManagementDomain.Context;
using BookingManagementDomain.EntityModels;
#endregion

#region Namespcae
namespace BookingManagementApplication.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        /// <summary>
        /// The booking management context
        /// </summary>
        private readonly BookingManagementContext _bookingManagementContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionRepository"/> class.
        /// </summary>
        /// <param name="bookingManagementContext">The booking management context.</param>
        public PermissionRepository(BookingManagementContext bookingManagementContext)
        {
            _bookingManagementContext = bookingManagementContext;
        }
        /// <summary>
        /// Deletes the permission.
        /// </summary>
        /// <param name="permissionId">The permission identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<bool> DeletePermissionAsync(string permissionId, CancellationToken cancellationToken = default)
        {
            bool isPermissionDeleted = false;
            using (var transaction = await _bookingManagementContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var permission = await _bookingManagementContext.Permissions.Where(x => x.UniqueId.ToString() == permissionId).FirstOrDefaultAsync(cancellationToken);
                    if (permission != null)
                    {
                        _bookingManagementContext.Permissions.Remove(permission);
                        var savedResult = await _bookingManagementContext.SaveChangesAsync(cancellationToken);
                        await transaction.CommitAsync(cancellationToken);
                        isPermissionDeleted = savedResult > 0 ? true : false;
                    }
                }
                catch
                {
                    await transaction.RollbackAsync();
                }
            }
            return isPermissionDeleted;
        }

        /// <summary>
        /// Gets all permissions asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public IQueryable<Permission> GetAllPermissionsAsync(CancellationToken cancellationToken = default)
        {
            return _bookingManagementContext.Permissions.OrderByDescending(x => x.CreatedDateTime);
        }

        /// <summary>
        /// Gets the permission asynchronous.
        /// </summary>
        /// <param name="permissionId">The permission identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Permission?> GetPermissionAsync(string permissionId, CancellationToken cancellationToken = default)
        {
            var permission = await _bookingManagementContext.Permissions.Where(x => x.UniqueId.ToString() == permissionId).FirstOrDefaultAsync(cancellationToken);
            return permission;
        }

        /// <summary>
        /// Saves the permission asynchronous.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Permission> SavePermissionAsync(Permission permission, CancellationToken cancellationToken = default)
        {
            EntityEntry<Permission> savedResult = null!;
            using (var transaction = await _bookingManagementContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    savedResult = await _bookingManagementContext.Permissions.AddAsync(permission, cancellationToken);
                    await _bookingManagementContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                }
                catch
                {
                    await transaction.RollbackAsync();
                }
            }

            return savedResult.Entity;
        }

        /// <summary>
        /// Updates the permissin asynchronous.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Permission> UpdatePermissinAsync(Permission permission, CancellationToken cancellationToken = default)
        {
            EntityEntry<Permission> savedResult = null!;
            using (var transaction = await _bookingManagementContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    savedResult = _bookingManagementContext.Permissions.Update(permission);
                    await _bookingManagementContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                }
                catch
                {
                    await transaction.RollbackAsync();
                }
            }

            return savedResult.Entity;
        }
    }
}
#endregion