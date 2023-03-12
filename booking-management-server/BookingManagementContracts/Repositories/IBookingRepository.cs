#region References
using BookingManagementCommon.DTO;
using BookingManagementDomain.EntityModels;
#endregion

#region Namespace
namespace BookingManagementContracts.Repositories
{
    public interface IBookingRepository
    {
        /// <summary>
        /// Saves the booking asynchronous.
        /// </summary>
        /// <param name="booking">The booking.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Booking> SaveBookingAsync(Booking booking, CancellationToken cancellationToken = default);
        /// <summary>
        /// Updates the booking asynchronous.
        /// </summary>
        /// <param name="booking">The booking.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Booking> UpdateBookingAsync(Booking booking, CancellationToken cancellationToken = default);
        /// <summary>
        /// Deletes the booking asynchronous.
        /// </summary>
        /// <param name="booking">The booking.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<bool> DeleteBookingAsync(string bookingId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets all bookings asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        IQueryable<Booking> GetAllBookingsAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the assigned bookings.
        /// </summary>
        /// <param name="assigneeId">The assignee identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<IEnumerable<Booking>> GetAssignedBookingsAsync(string assigneeId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the booking.
        /// </summary>
        /// <param name="bookingId">The booking identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Booking?> GetBookingAsync(string bookingId, CancellationToken cancellationToken = default);
    }
}
#endregion