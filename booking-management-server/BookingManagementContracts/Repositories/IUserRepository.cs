#region References
using BookingManagementDomain.EntityModels;
#endregion

#region Namespace
namespace BookingManagementContracts.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Saves the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<User> SaveUserAsync(User user, CancellationToken cancellationToken = default);
        /// <summary>
        /// Updates the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken = default);
        /// <summary>
        /// Deletes the user asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<bool> DeleteUserAsync(string userId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the user asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<User?> GetUserAsync(string userId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets all users asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        IQueryable<User> GetAllUsersAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the name of the user by user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<User?> GetUserByUserName(string userName, CancellationToken cancellationToken = default);
    }
}
#endregion