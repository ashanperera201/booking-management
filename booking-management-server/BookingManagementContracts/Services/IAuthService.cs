#region References
using BookingManagementCommon.DTO;
#endregion

#region Namespace
namespace BookingManagementContracts.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Authentications the login asynchronous.
        /// </summary>
        /// <param name="auth">The authentication.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<AuthResponseDto?> AuthLoginAsync(AuthDto auth, CancellationToken cancellationToken = default);
    }
}
#endregion