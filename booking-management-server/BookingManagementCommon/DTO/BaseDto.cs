#region Namespace
namespace BookingManagementCommon.DTO
{
    public class BaseDto
    {
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDateTime { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDateTime { get; set; }

    }
}
#endregion