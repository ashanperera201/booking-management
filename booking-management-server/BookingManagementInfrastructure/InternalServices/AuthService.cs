#region References
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookingManagementCommon.DTO;
using BookingManagementCommon.Utils;
using BookingManagementContracts.Repositories;
using BookingManagementContracts.Services;
using BookingManagementDomain.EntityModels;
#endregion

#region Namespace
namespace BookingManagementInfrastructure.InternalServices
{
    public class AuthService : IAuthService
    {
        /// <summary>
        /// The user repository
        /// </summary>
        private readonly IUserRepository _userRepository;
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService" /> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="configuration">The configuration.</param>
        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }
        public async Task<AuthResponseDto?> AuthLoginAsync(AuthDto auth, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetUserByUserName(auth.UserName, cancellationToken);
            if (user != null)
            {
                var keySize = int.Parse(_configuration.GetValue<string>("keySize"));
                var iterations = int.Parse(_configuration.GetValue<string>("Iterations"));

                var byteArray = Enumerable.Range(0, user.PasswordSalt.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(user.PasswordSalt.Substring(x, 2), 16)).ToArray();

                var isVerifiedUser = PasswordHashing.VerifyPassword(auth.Password, user.UserPassword, keySize, iterations, byteArray);

                if (isVerifiedUser)
                {
                    // GENERATE AUTH TOKEN
                    var token = _generateAuthToken(user);
                    return new AuthResponseDto
                    {
                        isAuthenticated = string.IsNullOrEmpty(token) ? false : true,
                        AuthToken = token,
                    };
                }
                else
                {
                    return null;
                }
            }
            return null;
        }


        /// <summary>
        /// Generates the authentication token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        private string _generateAuthToken(User user)
        {
            var key = _configuration["Jwt:Key"];
            var expirationHours = _configuration["Jwt:ExpirationHours"];
            if (key != null && expirationHours != null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[] {
                    new Claim("userId", user.UniqueId.ToString()),
                };
                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddHours(int.Parse(expirationHours)),
                    signingCredentials: credentials);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            return string.Empty;
        }
    }
}
#endregion