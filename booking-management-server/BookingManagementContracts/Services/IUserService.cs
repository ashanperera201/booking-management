#region References
using BookingManagementCommon.DTO;
using BookingManagementDomain.EntityModels;
#endregion

#region Namespace

namespace BookingManagementContracts.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Saves the User asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<UserDto> SaveUserAsync(UserDto user, CancellationToken cancellationToken = default);
        /// <summary>
        /// Updates the User asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<UserDto> UpdateUserAsync(UserDto user, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the User asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<UserDto?> GetUserAsync(string userId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets all users asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<UserDto>?> GetAllUsersAsync();
    }
}

#endregion