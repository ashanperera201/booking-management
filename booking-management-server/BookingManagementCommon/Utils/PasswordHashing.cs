#region References
using System.Security.Cryptography;
using System.Text;
#endregion

#region Namespace

namespace BookingManagementCommon.Utils
{
    public static class PasswordHashing
    {
        /// <summary>
        /// Hashes the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="keySize">Size of the key.</param>
        /// <param name="iterations">The iterations.</param>
        /// <param name="salt">The salt.</param>
        /// <returns></returns>
        public static string HashPassword(string password, int keySize, int iterations, out byte[] salt)
        {
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

            salt = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            return Convert.ToHexString(hash);
        }

        /// <summary>
        /// Verifies the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="hash">The hash.</param>
        /// <param name="keySize">Size of the key.</param>
        /// <param name="iterations">The iterations.</param>
        /// <param name="salt">The salt.</param>
        /// <returns></returns>
        public static bool VerifyPassword(string password, string hash, int keySize, int iterations, byte[] salt)
        {
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
            return hashToCompare.SequenceEqual(Convert.FromHexString(hash));
        }
    }
}
#endregion