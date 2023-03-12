#region Namespace
namespace BookingManagementCommon.DTO
{
    public class RoleDto : BaseDto
    {
        public int? Id { get; set; }
        public Guid? UniqueId { get; set; }
        public string RoleName { get; set; }
        public string? RoleDescription { get; set; }
        public IEnumerable<PermissionDto>? Permissions { get; set; }
        public IEnumerable<UserDto>? Users { get; set; }
    }
}
#endregion