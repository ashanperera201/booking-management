#region References
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;
using Microsoft.EntityFrameworkCore;
using BookingManagementContracts.Repositories;
using BookingManagementDomain.Context;
using BookingManagementDomain.EntityModels;
#endregion

#region Namespace
namespace BookingManagementApplication.Repositories
{
    public class UserRepository : IUserRepository
    {

        /// <summary>
        /// The booking management context
        /// </summary>
        private readonly BookingManagementContext _bookingManagementContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="bookingManagementContext">The booking management context.</param>
        public UserRepository(BookingManagementContext bookingManagementContext)
        {
            _bookingManagementContext = bookingManagementContext;
        }

        /// <summary>
        /// Deletes the user asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<bool> DeleteUserAsync(string userId, CancellationToken cancellationToken = default)
        {
            bool isDeleted = false;
            using (var transaction = await _bookingManagementContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var result = await _bookingManagementContext.Users.Where(x => x.UniqueId.ToString() == userId).FirstOrDefaultAsync(cancellationToken);
                    if (result != null)
                    {
                        _bookingManagementContext.Users.Remove(result);
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
        /// Gets all users asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public IQueryable<User> GetAllUsersAsync(CancellationToken cancellationToken = default)
        {
            return _bookingManagementContext.Users.Include(x => x.Role).OrderByDescending(x => x.CreatedDateTime);
        }

        /// <summary>
        /// Gets the user asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<User?> GetUserAsync(string userId, CancellationToken cancellationToken = default)
        {
            return await _bookingManagementContext.Users.Include(x => x.Role).ThenInclude(p => p.Permissions).Where(x => x.UniqueId.ToString() == userId).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Gets the name of the user by user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<User?> GetUserByUserName(string userName, CancellationToken cancellationToken = default)
        {
            return await _bookingManagementContext.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Saves the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<User> SaveUserAsync(User user, CancellationToken cancellationToken = default)
        {
            EntityEntry<User> savedResult = null!;
            using (var transaction = await _bookingManagementContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    savedResult = await _bookingManagementContext.Users.AddAsync(user, cancellationToken);
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
        /// Updates the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken = default)
        {
            EntityEntry<User> savedResult = null!;
            using (var transaction = await _bookingManagementContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var existsRecord = await _bookingManagementContext.Users.Where(x => x.Id == user.Id).FirstOrDefaultAsync();
                    if (existsRecord != null)
                    {
                        existsRecord.UserEmail = user.UserEmail;
                        existsRecord.UniqueId = user.UniqueId;
                        existsRecord.RoleId = user.RoleId;
                        existsRecord.UserName = user.UserName;
                        existsRecord.UserEmail = user.UserEmail;
                        existsRecord.UserContact = user.UserContact;
                        existsRecord.IsActive = user.IsActive;
                        existsRecord.UpdatedBy = user.UpdatedBy;
                        existsRecord.UpdatedDateTime = DateTime.UtcNow;

                        savedResult = _bookingManagementContext.Users.Update(existsRecord);
                        await _bookingManagementContext.SaveChangesAsync(cancellationToken);
                        await transaction.CommitAsync(cancellationToken);
                    }
                }
                catch
                {
                    await transaction.RollbackAsync();
                }


                savedResult.Entity.PasswordSalt = string.Empty;
                savedResult.Entity.UserPassword = string.Empty;

                return savedResult.Entity;
            }
        }
    }
}
#endregion