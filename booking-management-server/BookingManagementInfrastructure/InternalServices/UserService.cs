#region References
using BookingManagementCommon.DTO;
using BookingManagementCommon.Utils;
using BookingManagementContracts.Mapper;
using BookingManagementContracts.Repositories;
using BookingManagementContracts.Services;
using BookingManagementDomain.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security;
using System.Threading;
#endregion

#region Namespace
namespace BookingManagementInfrastructure.InternalServices
{
    public class UserService : IUserService
    {
        /// <summary>
        /// The user repository
        /// </summary>
        private readonly IUserRepository _userRepository;
        /// <summary>
        /// The entity mapper
        /// </summary>
        private readonly IEntityMapper _entityMapper;
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Initializes a new instance of the <see cref="UserService" /> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="entityMapper">The entity mapper.</param>
        /// <param name="configuration">The configuration.</param>

        public UserService(IUserRepository userRepository, IEntityMapper entityMapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _entityMapper = entityMapper;
            _configuration = configuration;
        }

        public async Task<IEnumerable<UserDto>?> GetAllUsersAsync()
        {
            var result = await (from user in _userRepository.GetAllUsersAsync()
                                select new User
                                {
                                    Id = user.Id,
                                    UniqueId = user.UniqueId,
                                    UserName = user.UserName,
                                    IsActive = user.IsActive,
                                    UserEmail = user.UserEmail,
                                    RoleId = user.RoleId,
                                    UserContact = user.UserContact,
                                    Role = user.Role,
                                }).ToListAsync();

            if (result != null && result.Count > 0)
            {
                return _entityMapper.Map<List<User>, List<UserDto>>(result);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the User asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<UserDto?> GetUserAsync(string userId, CancellationToken cancellationToken = default)
        {
            var result = await _userRepository.GetUserAsync(userId, cancellationToken);
             if (result != null)
            {
                result.PasswordSalt = string.Empty;
                result.UserPassword = string.Empty;
                return _entityMapper.Map<User, UserDto>(result);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Saves the User asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<UserDto> SaveUserAsync(UserDto user, CancellationToken cancellationToken = default)
        {
            user.UniqueId = Guid.NewGuid();

            // LOAD CONFIGS
            var keySize = int.Parse(_configuration.GetValue<string>("keySize"));
            var iterations = int.Parse(_configuration.GetValue<string>("Iterations"));

            // PASSWORD HASH
            var hash = PasswordHashing.HashPassword(user.UserPassword, keySize, iterations, out var salt);

            // ENTITY MAP
            var mappedEntitty = _entityMapper.Map<UserDto, User>(user);

            // ASSIGN PASSWORD WITH SALT
            mappedEntitty.UserPassword = hash;
            mappedEntitty.PasswordSalt = Convert.ToHexString(salt);
            mappedEntitty.CreatedDateTime = DateTime.UtcNow;

            var savedResult = await _userRepository.SaveUserAsync(mappedEntitty, cancellationToken);
            var returnResult = _entityMapper.Map<User, UserDto>(savedResult);

            returnResult.UserPassword = string.Empty;
            return returnResult;
        }

        /// <summary>
        /// Updates the User asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<UserDto> UpdateUserAsync(UserDto user, CancellationToken cancellationToken = default)
        {
            var mappedEntitty = _entityMapper.Map<UserDto, User>(user);
            mappedEntitty.UpdatedDateTime = DateTime.UtcNow;
            var savedResult = await _userRepository.UpdateUserAsync(mappedEntitty, cancellationToken);
            return _entityMapper.Map<User, UserDto>(savedResult);
        }
    }
}
#endregion