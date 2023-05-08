using System;
using System.Security.Cryptography;
using System.Text;
using Dragonfly.Core.Common;

namespace Dragonfly.Core.Security
{
    internal class OtpGenerator
    {
        private const int Digits = 6;

        private readonly DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public string GetOtp(string key, int duration = 60)
        {
            var iteration = (long)(DateTime.UtcNow - _unixEpoch).TotalSeconds / duration;
            return GenerateOtp(key, iteration);
        }

        private static string GenerateOtp(string key, long iterationNumber)
        {
            //Here the system converts the iteration number to a byte[]
            var iterationNumberByte = BitConverter.GetBytes(iterationNumber);
            //To BigEndian (MSB LSB)
            if (BitConverter.IsLittleEndian) Array.Reverse(iterationNumberByte);

            //Hash the userId by HMAC-SHA-1 (Hashed Message Authentication Code)
            var userIdByte = Encoding.ASCII.GetBytes(key);
            var userIdHmac = new HMACSHA1(userIdByte, true);
            var hash = userIdHmac.ComputeHash(iterationNumberByte); //Hashing a message with a secret key

            //RFC4226 http://tools.ietf.org/html/rfc4226#section-5.4
            var offset = hash[hash.Length - 1] & 0xf; //0xf = 15d
            var binary =
                ((hash[offset] & 0x7f) << 24)      //0x7f = 127d
                | ((hash[offset + 1] & 0xff) << 16) //0xff = 255d
                | ((hash[offset + 2] & 0xff) << 8)
                | (hash[offset + 3] & 0xff);

            var password = binary % (int)Math.Pow(10, Digits); // Shrink: 6 digits
            return password.ToString(new string('0', Digits));
        }
    }
}
