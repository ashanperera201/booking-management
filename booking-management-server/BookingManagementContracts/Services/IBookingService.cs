#region References
using BookingManagementCommon.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

#region Namespace
namespace BookingManagementContracts.Services
{
    public interface IBookingService
    {
        /// <summary>
        /// Saves the Booking asynchronous.
        /// </summary>
        /// <param name="booking">The booking.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<BookingDto> SaveBookingAsync(BookingDto booking, CancellationToken cancellationToken = default);
        /// <summary>
        /// Updates the Booking asynchronous.
        /// </summary>
        /// <param name="booking">The booking.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<BookingDto> UpdateBookingAsync(BookingDto booking, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets all Bookings asynchronous.
        /// </summary>
        /// <param name="pagination">The pagination.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<PaginationResponseDto<BookingDto>> GetAllBookingsAsync(PaginationDto pagination, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the assigned bookings.
        /// </summary>
        /// <param name="assigneeId">The assignee identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<IEnumerable<BookingDto>> GetAssignedBookingsAsync(string assigneeId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the Booking asynchronous.
        /// </summary>
        /// <param name="bookingId">The booking identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<BookingDto?> GetBookingAsync(string bookingId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Deletes the Booking asynchronous.
        /// </summary>
        /// <param name="bookingId">The booking identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<bool> DeleteBookingAsync(string bookingId, CancellationToken cancellationToken = default);
    }
}
#endregion