#region Namespace
namespace BookingManagementCommon.DTO
{
    public class AuthResponseDto
    {
        public bool isAuthenticated { get; set; }
        public string AuthToken { get; set; }
    }
}
#endregion