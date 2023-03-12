#region References
using FluentValidation;
#endregion


#region Namespace
namespace BookingManagementCommon.DTO
{
    public class PermissionDto : BaseDto
    {
        public int? Id { get; set; }
        public Guid? UniqueId { get; set; }
        public int? RoleId { get; set; }
        public string PermissionName { get; set; }
        public string? PermissionDescription { get; set; }
        public RoleDto? Role { get; set; }
    }


    public class PermissionValidator : AbstractValidator<PermissionDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionValidator"/> class.
        /// </summary>
        public PermissionValidator()
        {
            RuleFor(x => x.PermissionName).NotNull();
        }
    }
}
#endregion