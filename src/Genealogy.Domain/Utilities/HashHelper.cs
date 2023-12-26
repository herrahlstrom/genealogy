using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Genealogy.Domain.Utilities;

public static class HashHelper
{
   public static string EncryptString(string plainText, string salt) => EncryptWithPBKDF2(plainText, salt);

   private static string EncryptWithPBKDF2(string plainText, string salt)
   {
      const int iterations = 10000; // Number of iterations for PBKDF2
      const int derivedKeyLength = 512; // Length of the derived key in bits (64 bytes)

      byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

      using Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(plainText, saltBytes, iterations, HashAlgorithmName.SHA512);
      byte[] bytes = pbkdf2.GetBytes(derivedKeyLength / 8);

      return Convert.ToBase64String(bytes);
   }

}
