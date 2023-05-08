using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Dragonfly.Core.ArgumentGuard;
using Dragonfly.Core.Encryption;
using Dragonfly.Core.Formatters;

namespace Dragonfly.Core
{
    public static class StringExtensions
    {
        #region PrivateFields
        private static readonly byte[] EncryptionKey = Encoding.ASCII.GetBytes("9PAEu83G");
        #endregion

        public static string RemoveAtRight(this string value, int count)
        {
            return value.Substring(0, value.Length - count);
        }

        public static string GetNotNull(this string value)
        {
            return value ?? string.Empty;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "int")]
        public static int ToInt(this string value)
        {
            return int.Parse(value);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "long")]
        public static long ToLong(this string value)
        {
            return long.Parse(value);
        }

        public static decimal ToDecimal(this string value)
        {
            var culture = CultureInfo.InvariantCulture;
            return decimal.Parse(value, culture);
        }

        public static bool IsEmpty(this string value)
        {
            return value == string.Empty;
        }

        public static bool IsEmptyOrNull(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static string XmlDecode(this string value)
        {
            return value.IsEmptyOrNull()
               ? value
               : value.Replace(@"&lt;", @"<")
                  .Replace(@"&gt;", @">")
                  .Replace(@"&quot;", "\"")
                  .Replace(@"&amp;", @"&");
        }

        public static DateTime ToDate(this string value)
        {
            var culture = CultureInfo.InvariantCulture;
            return DateTime.Parse(value, culture);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "bool")]
        public static bool ToBool(this string value)
        {
            switch (value)
            {
                case "0":
                    return false;
                case "1":
                    return true;
                default:
                    return bool.Parse(value);
            }
        }

        public static Guid ToGuid(this string value)
        {
            return Guid.Parse(value);
        }

        public static byte[] HexToByte(this string value)
        {
            var hex = value.Substring(2, value.Length - 2).ToUpper();

            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");

            var len = hex.Length / 2;
            var arr = new byte[len];

            for (var i = 0; i < len; ++i)
            {
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + GetHexVal(hex[(i << 1) + 1]));
            }

            return arr;
        }

        private static int GetHexVal(char hex)
        {
            var val = (int)hex;
            return val - (val < 58 ? 48 : 55);
        }

        public static string Compress(this string value)
        {
            var bytes = Encoding.Unicode.GetBytes(value);
            using (var msi = new MemoryStream(bytes))
            {
                var mso = new MemoryStream();
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    msi.CopyTo(gs);
                }
                var output = mso.ToArray();
                return Convert.ToBase64String(output);
            }
        }

        public static string Decompress(this string value)
        {
            var bytes = Convert.FromBase64String(value);
            using (var mso = new MemoryStream())
            {
                var msi = new MemoryStream(bytes);
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    gs.CopyTo(mso);
                }
                return Encoding.Unicode.GetString(mso.ToArray());
            }
        }

        public static string Surround(this string value, string delimiter)
        {
            return string.Format("{0}{1}{0}", delimiter, value);
        }

        public static string SurroundIfHasBlanks(this string value, string delimiter)
        {
            Guard.Check(value).IsNotNull();
            Guard.Check(delimiter).IsNotNullOrEmpty();

            return value.Contains(" ") ? value.Surround(delimiter) : value;
        }

        public static string Inject(this string format, params object[] formattingArgs)
        {
            return string.Format(format, formattingArgs);
        }

        public static string Inject(this string format, params string[] formattingArgs)
        {
            return string.Format(format, formattingArgs.Select(a => a as object).ToArray());
        }

        public static string ToString(this string @this, object @object, string startSeparator = "{", string endSeparator = "}")
        {
            if (@object == null)
                return @this;

            @object.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.CanRead)
                .ForEach(x =>
                {
                    var value = x.GetValue(@object, null);
                    @this = @this.Replace(startSeparator + x.Name + endSeparator, value?.ToString() ?? "");
                });

            return @this;
        }

        public static string ToString(this string @this, object @object, char prefix)
        {
            if (@object == null)
                return @this;

            @object.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.CanRead)
                .ForEach(x =>
                {
                    var value = x.GetValue(@object, null);
                    @this = @this.Replace(prefix + x.Name, value?.ToString() ?? "");
                });

            return @this;
        }

        public static string ToString(this string @this, params KeyValuePair<string, string>[] pairs)
        {
            return string.IsNullOrEmpty(@this)
                ? @this
                : pairs.Aggregate(@this, (current, pair) => current.Replace(pair.Key, pair.Value));
        }

        public static string ToString(this string @this, string format, IStringFormatter formatter = null)
        {
            return formatter.Check(new GenericStringFormatter()).Format(@this, format);
        }

        public static string EncryptRsa(this string @this, string key)
        {
            var cspp = new CspParameters { KeyContainerName = key };
            var rsa = new RSACryptoServiceProvider(cspp) { PersistKeyInCsp = true };
            var bytes = rsa.Encrypt(Encoding.UTF8.GetBytes(@this), true);

            return BitConverter.ToString(bytes);
        }

        public static string DecryptRsa(this string @this, string key)
        {
            var cspp = new CspParameters { KeyContainerName = key };
            var rsa = new RSACryptoServiceProvider(cspp) { PersistKeyInCsp = true };
            var decryptArray = @this.Split(new[] { "-" }, StringSplitOptions.None);
            var decryptByteArray = Array.ConvertAll(decryptArray, (s => Convert.ToByte(byte.Parse(s, NumberStyles.HexNumber))));
            var bytes = rsa.Decrypt(decryptByteArray, true);

            return Encoding.UTF8.GetString(bytes);
        }

        public static string EncryptAes(this string @this, string key)
        {
            return RijndaelManagedEncryption.EncryptRijndael(@this, key);
        }

        public static string DecryptAes(this string @this, string key)
        {
            return RijndaelManagedEncryption.DecryptRijndael(@this, key);
        }

        public static string Encrypt(this string @this)
        {
            if (string.IsNullOrEmpty(@this))
            {
                throw new ArgumentNullException(nameof(@this), "The string which needs to be encrypted can not be null.");
            }

            var cryptoProvider = new DESCryptoServiceProvider();
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(EncryptionKey, EncryptionKey), CryptoStreamMode.Write);

            var writer = new StreamWriter(cryptoStream);
            writer.Write(@this);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();

            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }

        public static string Decrypt(this string @this)
        {
            if (string.IsNullOrEmpty(@this))
            {
                throw new ArgumentNullException(nameof(@this), "The string which needs to be decrypted can not be null.");
            }

            var cryptoProvider = new DESCryptoServiceProvider();
            var memoryStream = new MemoryStream(Convert.FromBase64String(@this));
            var cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(EncryptionKey, EncryptionKey), CryptoStreamMode.Read);
            var reader = new StreamReader(cryptoStream);

            return reader.ReadToEnd();
        }
    }
}

