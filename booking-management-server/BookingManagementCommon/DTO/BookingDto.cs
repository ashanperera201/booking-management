#region Namespace
namespace BookingManagementCommon.DTO
{
    public class BookingDto : BaseDto
    {
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public string FromAddress { get; set; } = null!;
        public string ToAddress { get; set; } = null!;
        public string GoodType { get; set; } = null!;
        public decimal Weight { get; set; }
        public int Pricing { get; set; }
        public DateTime BookedTime { get; set; }
        public string AssignedDriver { get; set; } = null!;
    }
}
#endregion