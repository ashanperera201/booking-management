#region References
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using BookingManagementContracts.Repositories;
using BookingManagementDomain.Context;
using BookingManagementDomain.EntityModels;
#endregion

#region Namespace
namespace BookingManagementApplication.Repositories
{
    public class RoleRepository : IRoleRepository
    {

        /// <summary>
        /// The booking management context
        /// </summary>
        private readonly BookingManagementContext _bookingManagementContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository"/> class.
        /// </summary>
        /// <param name="bookingManagementContext">The booking management context.</param>
        public RoleRepository(BookingManagementContext bookingManagementContext)
        {
            _bookingManagementContext = bookingManagementContext;
        }

        /// <summary>
        /// Deletes the role asynchronous.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<bool> DeleteRoleAsync(string roleId, CancellationToken cancellationToken = default)
        {
            bool isDeleted = false;
            using (var transaction = await _bookingManagementContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var result = await _bookingManagementContext.Roles.Where(x => x.UniqueId.ToString() == roleId).FirstOrDefaultAsync(cancellationToken);
                    if (result != null)
                    {
                        _bookingManagementContext.Roles.Remove(result);
                        var savedResult = await _bookingManagementContext.SaveChangesAsync(cancellationToken);
                        await transaction.CommitAsync(cancellationToken);
                        isDeleted = savedResult > 0 ? true : false;
                    }
                }
                catch
                {
                    await transaction.RollbackAsync();
                }
            }
            return isDeleted;
        }

        /// <summary>
        /// Gets the role asynchronous.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Role> GetRoleAsync(string roleId, CancellationToken cancellationToken = default)
        {
            return await _bookingManagementContext.Roles.Where(x => x.UniqueId.ToString() == roleId).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Gets the role details asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public IQueryable<Role> GetRoleDetailsAsync(CancellationToken cancellationToken = default)
        {
            return _bookingManagementContext.Roles.Include(p => p.Permissions).OrderByDescending(x => x.CreatedDateTime);
        }

        /// <summary>
        /// Saves the role asynchronous.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Role> SaveRoleAsync(Role role, CancellationToken cancellationToken = default)
        {
            EntityEntry<Role> savedResult = null!;
            using (var transaction = await _bookingManagementContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    savedResult = await _bookingManagementContext.Roles.AddAsync(role, cancellationToken);
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
        /// Updates the role asynchronous.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Role> UpdateRoleAsync(Role role, CancellationToken cancellationToken = default)
        {
            EntityEntry<Role> savedResult = null!;
            using (var transaction = await _bookingManagementContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    savedResult = _bookingManagementContext.Roles.Update(role);
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