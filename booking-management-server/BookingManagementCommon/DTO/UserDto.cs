#region Namespace
namespace BookingManagementCommon.DTO
{
    public class UserDto : BaseDto  
    {
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public int? RoleId { get; set; }
        public string UserName { get; set; } = null!;
        public string? UserPassword { get; set; } = null!;
        public string? UserEmail { get; set; } = null!;
        public string? UserContact { get; set; } = null!;
        public RoleDto? Role { get; set; }
    }
}
#endregion