#region Namespace
namespace BookingManagementCommon.DTO
{
    public class PaginationResponseDto<Entity> where Entity : class
    {
        public int total { get; set; }
        public int Page { get; set; }
        public List<Entity> Records { get; set; }
    }
}
#endregion