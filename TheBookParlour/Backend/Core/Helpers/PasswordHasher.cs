using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using TheBookParlour.Core.Interfaces;

namespace TheBookParlour.Core.Helpers
{
    public sealed class PasswordHasher: IPasswordHasher
        //Sealed = man få ej ärva från klassen
    {
        private const int SaltSize = 16; //Rekommenderat värde
        private const int HashSize = 32; //Rekommenderat värde
        private const int Iterations = 100000;

        private readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;//inbyggd .NET-algoritm

        public string Hash(string password) {

            //byte- datavärde som kan hålla 0-255 bitar, 1 byte = 8 bitar. Säkrare än string. 

            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

            return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
        }

        public bool Verify(string password, string passwordHash)
        {   
            // Delar upp passwordHash i två delar
            string[] parts = passwordHash.Split('-');
            byte[] hash = Convert.FromHexString(parts[0]);
            byte[] salt = Convert.FromHexString(parts[1]);

            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

            return CryptographicOperations.FixedTimeEquals(hash, inputHash);
        }
    }

   
}
