#region References
using BookingManagementContracts.Repositories;
using BookingManagementDomain.Context;
using BookingManagementDomain.EntityModels;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;
using Microsoft.EntityFrameworkCore;

#endregion

#region Namespace
namespace BookingManagementApplication.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        /// <summary>
        /// The booking management context
        /// </summary>
        private readonly BookingManagementContext _bookingManagementContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="BookingRepository" /> class.
        /// </summary>
        /// <param name="bookingManagementContext">The booking management context.</param>
        public BookingRepository(BookingManagementContext bookingManagementContext)
        {
            _bookingManagementContext = bookingManagementContext;
        }

        /// <summary>
        /// Deletes the booking asynchronous.
        /// </summary>
        /// <param name="bookingId"></param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<bool> DeleteBookingAsync(string bookingId, CancellationToken cancellationToken = default)
        {
            bool isDeleted = false;
            using (var transaction = await _bookingManagementContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var result = await _bookingManagementContext.Bookings.Where(x => x.UniqueId.ToString() == bookingId).FirstOrDefaultAsync(cancellationToken);
                    if (result != null)
                    {
                        _bookingManagementContext.Bookings.Remove(result);
                        var savedResult = await _bookingManagementContext.SaveChangesAsync(cancellationToken);
                        await transaction.CommitAsync(cancellationToken);
                        isDeleted = savedResult > 0 ? true : false;
                    }
                }
                catch
                {
                    await transaction.RollbackAsync();
                }
            }
            return isDeleted;
        }

        /// <summary>
        /// Gets all bookings asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public IQueryable<Booking> GetAllBookingsAsync(CancellationToken cancellationToken = default)
        {
            return _bookingManagementContext.Bookings.OrderByDescending(x => x.CreatedDateTime);
        }

        /// <summary>
        /// Gets the assigned bookings.
        /// </summary>
        /// <param name="assigneeId">The assignee identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Booking>> GetAssignedBookingsAsync(string assigneeId, CancellationToken cancellationToken = default)
        {
            return await _bookingManagementContext.Bookings.Where(x => x.AssignedDriver == assigneeId).OrderByDescending(x => x.CreatedDateTime).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Gets the booking.
        /// </summary>
        /// <param name="bookingId">The booking identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Booking?> GetBookingAsync(string bookingId, CancellationToken cancellationToken = default)
        {
            return await _bookingManagementContext.Bookings.Where(x => x.UniqueId.ToString() == bookingId).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Saves the booking asynchronous.
        /// </summary>
        /// <param name="booking">The booking.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Booking> SaveBookingAsync(Booking booking, CancellationToken cancellationToken = default)
        {
            EntityEntry<Booking> savedResult = null!;
            using (var transaction = await _bookingManagementContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    savedResult = await _bookingManagementContext.Bookings.AddAsync(booking, cancellationToken);
                    await _bookingManagementContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                }
                catch
                {
                    await transaction.RollbackAsync();
                }
            }
            return savedResult.Entity;
        }

        /// <summary>
        /// Updates the booking asynchronous.
        /// </summary>
        /// <param name="booking">The booking.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Booking> UpdateBookingAsync(Booking booking, CancellationToken cancellationToken = default)
        {
            EntityEntry<Booking> savedResult = null!;
            using (var transaction = await _bookingManagementContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    savedResult = _bookingManagementContext.Bookings.Update(booking);
                    await _bookingManagementContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                }
                catch
                {
                    await transaction.RollbackAsync();
                }
            }

            return savedResult.Entity; 
        }
    }
}
#endregion