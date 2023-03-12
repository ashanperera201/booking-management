#region References
using BookingManagementCommon.DTO;
using BookingManagementContracts.Mapper;
using BookingManagementContracts.Repositories;
using BookingManagementContracts.Services;
using BookingManagementDomain.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

#region Namespace
namespace BookingManagementInfrastructure.InternalServices
{
    public class BookingService : IBookingService
    {
        /// <summary>
        /// The booking repository
        /// </summary>
        private readonly IBookingRepository _bookingRepository;
        /// <summary>
        /// The entity mapper
        /// </summary>
        private readonly IEntityMapper _entityMapper;
        /// <summary>
        /// Initializes a new instance of the <see cref="BookingService"/> class.
        /// </summary>
        /// <param name="bookingRepository">The booking repository.</param>
        /// <param name="entityMapper">The entity mapper.</param>
        public BookingService(IBookingRepository bookingRepository, IEntityMapper entityMapper)
        {
            _bookingRepository = bookingRepository;
            _entityMapper = entityMapper;
        }
        /// <summary>
        /// Deletes the Booking asynchronous.
        /// </summary>
        /// <param name="bookingId">The booking identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<bool> DeleteBookingAsync(string bookingId, CancellationToken cancellationToken = default)
        {
            var result = await _bookingRepository.DeleteBookingAsync(bookingId, cancellationToken);
            return result;
        }

        /// <summary>
        /// Gets all Bookings asynchronous.
        /// </summary>
        /// <param name="pagination">The pagination.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<PaginationResponseDto<BookingDto>> GetAllBookingsAsync(PaginationDto pagination, CancellationToken cancellationToken = default)
        {
            var bookings = _bookingRepository.GetAllBookingsAsync(cancellationToken);
            var totalCount = await bookings.CountAsync();
            var bookingList = await (from booking in bookings select booking)
                               .OrderByDescending(x => x.CreatedDateTime)
                               .Skip((pagination.Page - 1) * pagination.PageSize)
                               .Take(pagination.PageSize)
                               .ToListAsync();


            return new PaginationResponseDto<BookingDto>
            {
                Page = pagination.Page,
                total = totalCount,
                Records = _entityMapper.Map<List<Booking>, List<BookingDto>>(bookingList)
            };
        }

        /// <summary>
        /// Gets the assigned bookings.
        /// </summary>
        /// <param name="assigneeId">The assignee identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<IEnumerable<BookingDto>> GetAssignedBookingsAsync(string assigneeId, CancellationToken cancellationToken = default)
        {
            var result = await _bookingRepository.GetAssignedBookingsAsync(assigneeId, cancellationToken);
            return _entityMapper.Map<IEnumerable<Booking>, IEnumerable<BookingDto>>(result);
        }

        /// <summary>
        /// Gets the Booking asynchronous.
        /// </summary>
        /// <param name="bookingId">The booking identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<BookingDto?> GetBookingAsync(string bookingId, CancellationToken cancellationToken = default)
        {
            var result = await _bookingRepository.GetBookingAsync(bookingId, cancellationToken);
            if(result != null)
            {
                return _entityMapper.Map<Booking, BookingDto>(result);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Saves the Booking asynchronous.
        /// </summary>
        /// <param name="booking">The booking.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<BookingDto> SaveBookingAsync(BookingDto booking, CancellationToken cancellationToken = default)
        {
            booking.UniqueId = Guid.NewGuid();
            booking.CreatedDateTime = DateTime.UtcNow;
            var mappedEntitty = _entityMapper.Map<BookingDto, Booking>(booking);
            var savedResult = await _bookingRepository.SaveBookingAsync(mappedEntitty, cancellationToken);
            return _entityMapper.Map<Booking, BookingDto>(savedResult);
        }

        public async Task<BookingDto> UpdateBookingAsync(BookingDto booking, CancellationToken cancellationToken = default)
        {
            booking.UpdatedDateTime = DateTime.UtcNow;
            var mappedEntitty = _entityMapper.Map<BookingDto, Booking>(booking);
            var savedResult = await _bookingRepository.UpdateBookingAsync(mappedEntitty, cancellationToken);
            return _entityMapper.Map<Booking, BookingDto>(savedResult);
        }
    }
}
#endregion
