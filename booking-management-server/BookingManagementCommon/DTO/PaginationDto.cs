#region References
namespace BookingManagementCommon.DTO
{
    public class PaginationDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? SearchTerm { get; set; }
    }
}
#endregion