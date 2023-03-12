#region References
using BookingManagementCommon.DTO;
using BookingManagementContracts.Services;
using BookingManagementDomain.EntityModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
#endregion

#region Namespace
namespace BookingManagementApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/booking")]   
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BookingController : BaseController
    {
        /// <summary>
        /// The booking service
        /// </summary>
        private readonly IBookingService _bookingService;
        /// <summary>
        /// Initializes a new instance of the <see cref="BookingController" /> class.
        /// </summary>
        /// <param name="bookingService">The booking service.</param>
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        /// <summary>
        /// Saves the booking asynchronous.
        /// </summary>
        /// <param name="booking">The booking.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveBookingAsync([FromBody] BookingDto booking, CancellationToken cancellationToken)
        {
            try
            {
                var savedResult = await _bookingService.SaveBookingAsync(booking, cancellationToken);
                return StatusCode(statusCode: savedResult != null ? StatusCodes.Status200OK : StatusCodes.Status500InternalServerError, savedResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Updates the booking asynchronous.
        /// </summary>
        /// <param name="booking">The booking.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateBookingAsync([FromBody] BookingDto booking, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _bookingService.UpdateBookingAsync(booking, cancellationToken);
                return StatusCode(statusCode: result != null ? StatusCodes.Status200OK : StatusCodes.Status500InternalServerError, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets all booking asynchronous.
        /// </summary>
        /// <param name="pagination">The pagination.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet("query")]      
        public async Task<IActionResult> GetAllBookingAsync([FromQuery] PaginationDto pagination, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _bookingService.GetAllBookingsAsync(pagination, cancellationToken);
                return StatusCode(statusCode: result != null ? StatusCodes.Status200OK : StatusCodes.Status204NoContent, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the permission asynchronous.
        /// </summary>
        /// <param name="bookingId">The booking identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetPermissionAsync(string bookingId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _bookingService.GetBookingAsync(bookingId, cancellationToken);
                return StatusCode(statusCode: result != null ? StatusCodes.Status200OK : StatusCodes.Status204NoContent, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Deletes the booking asynchronous.
        /// </summary>
        /// <param name="bookingId">The booking identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpDelete("{bookingId}")]
        public async Task<IActionResult> DeleteBookingAsync(string bookingId, CancellationToken cancellationToken)
        {
            try
            {
                var servicesRes = await _bookingService.DeleteBookingAsync(bookingId, cancellationToken);
                return StatusCode(servicesRes ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, servicesRes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the bookings by assignee identifier asynchronous.
        /// </summary>
        /// <param name="assigneeId">The assignee identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet("by-assignee/{assigneeId}")]
        public async Task<IActionResult> GetBookingsByAssigneeIdAsync(string assigneeId, CancellationToken cancellationToken)
        {
            try
            {
                var servicesRes = await _bookingService.GetAssignedBookingsAsync(assigneeId, cancellationToken);
                return StatusCode(servicesRes != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, servicesRes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
#endregion
